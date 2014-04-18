using OpenRiaServices.DomainServices.Tools;
using OpenRiaServices.DomainServices.Tools.TextTemplate.CSharpGenerators;

namespace RIAServices.M2M.EntityGenerator
{
    [DomainServiceClientCodeGenerator("M2M4RiaCodeGenerator", "C#")]
    public class M2M4RiaCodeGenerator : CSharpClientCodeGenerator
    {
        #region Properties

        protected override OpenRiaServices.DomainServices.Tools.TextTemplate.EntityGenerator EntityGenerator
        {
            get { return new M2M4RiaEntityGenerator(); }
        }

        #endregion
    }
}