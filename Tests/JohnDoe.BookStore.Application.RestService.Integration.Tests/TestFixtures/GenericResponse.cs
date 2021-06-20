using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.RestService.Integration.Tests.TestFixtures
{
    public class GenericResponse
    {
        private string status;
        public GenericResponse()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResponse"/> class.
        /// </summary>
        /// <param name="status">Response status. Can be S (Success), E (Error), W (Warning).</param>
        public GenericResponse(string status)
            : this(status, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResponse"/> class.
        /// </summary>
        /// <param name="status">Response status. Can be S (Success), E (Error), W (Warning).</param>
        /// <param name="code"> defined Status code.</param>
        /// <param name="message">Status message.</param>
        public GenericResponse(string status, string? code, string? message)
        {
            this.Status = status;
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Response status. Can be S (Success), F (Failed), W (Warning).
        /// </summary>
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                if ("S".Equals(this.status))
                {
                    if (string.IsNullOrEmpty(this.Code))
                    {
                        this.Code = "S0000";
                        this.Message = "Success";
                    }
                }
            }
        }

        /// <summary>
        ///  defined Status code.
        /// </summary>

        public string? Code { get; set; }

        /// <summary>
        /// Status message.
        /// </summary>

        public string? Message { get; set; }

    }
}
