namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class ParameterPrefixAttribute : Attribute
    {
        public string Prefix { get; }

        public ParameterPrefixAttribute(string prefix)
        {
            this.Prefix = prefix;
        }
    }
}
