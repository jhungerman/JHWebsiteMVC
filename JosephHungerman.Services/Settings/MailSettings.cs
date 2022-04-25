namespace JosephHungerman.Services.Settings
{
    public class MailSettings
    {
        public virtual string Mail { get; set; }
        public virtual string ToMail { get; set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
    }
}
