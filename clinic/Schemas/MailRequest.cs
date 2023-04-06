namespace clinic.Schemas
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; } = "Rs.Ge Email Verification";
        public string? Body { get; set; }
    }
}
