namespace DebitExpress.Mvvm
{
    public class ActionResult
    {
        public Result Result { get; set; }
        public string Message { get; set; }
    }

    public enum Result
    {
        NoChanges = 0,
        Successful = 1,
        Failed = -1
    }
}
