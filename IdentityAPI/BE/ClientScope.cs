namespace IdentityAPI.BE
{
    public partial class ClientScope
    {
        public string ClientId { get; set; }
        public int ScopeId { get; set; }

        public Client Client { get; set; }
        public Scope Scope { get; set; }
    }
}
