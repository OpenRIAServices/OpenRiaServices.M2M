using Microsoft.ServiceModel.DomainServices.Tools;
using Microsoft.ServiceModel.DomainServices.Tools.TextTemplate.CSharpGenerators;

namespace RIAServices.M2M.EntityGenerator
{
    [DomainServiceClientCodeGenerator("M2M4RiaCodeGenerator", "C#")]
    public class M2M4RiaCodeGenerator : CSharpClientCodeGenerator
    {
        #region Properties

        protected override Microsoft.ServiceModel.DomainServices.Tools.TextTemplate.EntityGenerator EntityGenerator
        {
            get { return new M2M4RiaEntityGenerator(); }
        }

        #endregion
    }
}