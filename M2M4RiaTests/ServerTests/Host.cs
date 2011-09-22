using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.CodeDom;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TextTemplating;

// Code based on download from
// http://timcools.net/post/2009/03/09/Template-based-code-generation-with-T4.aspx

namespace ServerTests
{
    /// <summary>
    /// Custom T4 host that can be used in custom developed applications.
    /// </summary>
    public class Host : MarshalByRefObject, ITextTemplatingEngineHost
    {
        #region Private fields

        private readonly AppDomain appDomain;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the standard assembly referenced to compile the template.
        /// </summary>
        /// <value>The standard assembly references.</value>
        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new List<string>
                {
                    typeof (CodeCompileUnit).Assembly.Location, //System.dll
                    typeof (string).Assembly.Location,          //mscorelib
                    Assembly.GetExecutingAssembly().Location    //current assembly
                };
            }
        }

        /// <summary>
        /// Gets the standard imports (usings) used to compile the template.
        /// </summary>
        /// <value>The standard imports.</value>
        public IList<string> StandardImports
        {
            get
            {
                return new List<string>
                {
                    typeof (String).Namespace,       //System
                    typeof (CodeObject).Namespace,   //System.CodeDom
                    typeof (CallContext).Namespace,  //System.Runtime.Remoting
                    typeof (Host).Namespace          //Current namespace
                };
            }
        }

        /// <summary>
        /// Gets the template file.
        /// </summary>
        /// <value>The template file.</value>
        public string TemplateFile
        {
            get
            {
                //Not used, template is not always comming from a file
                return string.Empty;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Host"/> class.
        /// </summary>
        public Host()
        {
            appDomain = AppDomain.CurrentDomain;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Host"/> class.
        /// </summary>
        /// <param name="appDomain">The app domain wherein the transformations are runned.</param>
        public Host( AppDomain appDomain )
        {
            this.appDomain = appDomain;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the host option. The enables communication from engine and
        /// template by passing object from the host to the called.
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <returns></returns>
        public object GetHostOption( string optionName )
        {
            if( optionName == "CacheAssemblies" )   // Asked by the engine
            {
                //This enables that each template is only compiled once.
                //Next time the template is generated, the same generated
                //class is used. 
                //The compiled classes are identified by using the MD5 hash
                //of the parsed template blocks
                return true;
            }

            return null;
        }

        /// <summary>
        /// Loads the include text.
        /// </summary>
        /// <param name="requestFileName">Name of the request file.</param>
        /// <param name="content">The content.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public bool LoadIncludeText( string requestFileName, out string content, out string location )
        {
            content = string.Empty;
            location = string.Empty;

            if( Path.IsPathRooted( requestFileName ) )
            {
                if( File.Exists( requestFileName ) )
                {
                    content = File.ReadAllText( requestFileName );
                    location = requestFileName;
                    return true;
                }
                return false;
            }


            string fullPath = Path.Combine( Assembly.GetEntryAssembly().Location, requestFileName );
            if( File.Exists( fullPath ) )
            {
                content = File.ReadAllText( requestFileName );
                location = fullPath;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Log the compilation errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void LogErrors( CompilerErrorCollection errors )
        {
            if( errors.Count == 0 )
            {
                return;
            }

            Console.WriteLine( "Errors occured during compilation:" );
            foreach( CompilerError error in errors )
            {
                Console.WriteLine( "Line: {0}  Columns: {1}  ({2}) {3}",
                    error.Line, error.Column, error.ErrorNumber, error.ErrorText );
            }
        }

        /// <summary>
        /// Provides the templating app domain.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public AppDomain ProvideTemplatingAppDomain( string content )
        {
            //Uncomment to see the generated class definitions
            //Console.WriteLine();
            //Console.WriteLine("--BEGIN TEMPLATE CLASS DEFINITION--");
            //Console.WriteLine(content);
            //Console.WriteLine("--END TEMPLATE CLASS DEFINITION--");
            //Console.WriteLine();

            return appDomain;
        }

        /// <summary>
        /// Resolves the assembly reference.
        /// </summary>
        /// <param name="assemblyReference">The assembly reference.</param>
        /// <returns></returns>
        public string ResolveAssemblyReference( string assemblyReference )
        {
            return ResolvePath( assemblyReference );
        }

        /// <summary>
        /// Resolves the property directive processor.
        /// </summary>
        /// <param name="processorName">Name of the processor.</param>
        /// <returns></returns>
        public Type ResolveDirectiveProcessor( string processorName )
        {
            throw new NotSupportedException(
                string.Format( "Directive processor {0} not supported!", processorName ) );
        }

        /// <summary>
        /// Resolves the parameter value.
        /// </summary>
        /// <param name="directiveId">The directive id.</param>
        /// <param name="processorName">Name of the processor.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public string ResolveParameterValue( string directiveId, string processorName, string parameterName )
        {
            return null;
        }

        /// <summary>
        /// Resolves the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string ResolvePath( string path )
        {
            if( Path.IsPathRooted( path ) )
            {
                return path;
            }

            string fullName = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location), path );
            if( File.Exists( fullName ) )
            {
                return fullName;
            }

            return path;
        }

        /// <summary>
        /// Sets the file extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        public void SetFileExtension( string extension )
        {
            //Setting the file extension is not supported by current host.
        }

        /// <summary>
        /// Sets the output encoding.
        /// </summary>
        public void SetOutputEncoding( Encoding encoding, bool fromOutputDirective )
        {
            //Setting output encoding is not supported by current host.
        }

        #endregion
    }
}
