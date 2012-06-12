namespace RIAServices.M2M
{
    using System;

    internal class LinkTableViewAttribute : Attribute
    {
        #region Public Properties

        public Type ElementType { get; set; }

        public Type LinkTableType { get; set; }

        public string M2MPropertyName { get; set; }

        #endregion
    }
}