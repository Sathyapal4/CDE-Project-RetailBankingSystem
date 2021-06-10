namespace RulesAPI.Models
{
    public enum Status
    {
        Allowed = 0,
        Denied = 1
    }
    public class RuleStatus
    {
        public Status Status { get; set; }
    }
}
