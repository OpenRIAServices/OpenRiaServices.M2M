using OpenRiaServices.Tools;
using OpenRiaServices.Tools.TextTemplate.CSharpGenerators;

namespace OpenRiaServices.M2M.EntityGenerator
{
    [DomainServiceClientCodeGenerator("M2M4RiaCodeGenerator", "C#")]
    public class M2M4RiaCodeGenerator : CSharpClientCodeGenerator
    {
        #region Properties

        protected override Tools.TextTemplate.EntityGenerator EntityGenerator
        {
            get { return new M2M4RiaEntityGenerator(); }
        }

        #endregion
    }
}