namespace RIAServices.M2M.EntityGenerator
{
    using Microsoft.ServiceModel.DomainServices.Tools;
    using Microsoft.ServiceModel.DomainServices.Tools.TextTemplate;
    using Microsoft.ServiceModel.DomainServices.Tools.TextTemplate.CSharpGenerators;

    [DomainServiceClientCodeGenerator("M2M4RiaCodeGenerator", "C#")]
    public class M2M4RiaCodeGenerator : CSharpClientCodeGenerator
    {
        #region Properties

        protected override EntityGenerator EntityGenerator
        {
            get
            {
                return new M2M4RiaEntityGenerator();
            }
        }

        #endregion
    }
}