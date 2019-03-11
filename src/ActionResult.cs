namespace DebitExpress.Mvvm
{
   public class ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class by using an error object. 
        /// </summary>
        /// <param name="isSuccessful">
        /// Indicate whether action taken was successful or not.
        /// </param>
        /// <param name="errorContent">
        /// If null <see cref="ActionResult"/>,  <see cref="ActionResult.ErrorContent"/> will return
        /// <see cref="string.Empty"/>.
        /// </param>
        public ActionResult(bool isSuccessful, object errorContent)
        {
            IsSuccessful = isSuccessful;
            _errorContent = errorContent;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class by using an error message. 
        /// </summary>
        /// <param name="isSuccessful">
        /// Specify whether action taken was successful or not.
        /// </param>
        /// <param name="errorMessage"></param>
        public ActionResult(bool isSuccessful, string errorMessage)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class and return <see cref="ActionResult.IsSuccessful"/> equal to <see langword="true"/>.
        /// </summary>
        public ActionResult()
        {
            IsSuccessful = true;
        }

        /// <summary>
        /// Returns the result of an action if successful or not.
        /// </summary>
        public bool IsSuccessful { get; }

        private readonly object _errorContent;
        /// <summary>
        /// Returns the error <see langword="object"/> of an action.
        /// </summary>
        public object ErrorContent => _errorContent ?? ErrorMessage;
        /// <summary>
        /// Returns the error message of an action.
        /// </summary>
        public string ErrorMessage { get; }
    }
}
