using Microsoft.AspNetCore.Mvc;

namespace Participants.Api.DTOs.CurrentAssignment
{
    public sealed class HuntFinishedActionResult : ActionResult
    {
        private readonly int _statusCode;
        private readonly string _content;

        public HuntFinishedActionResult(int statusCode, string content = null)
        {
            _statusCode = statusCode;
            _content = content;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = _statusCode;

            if (!string.IsNullOrEmpty(_content))
            {
                response.ContentType = "application/json";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(_content);
                response.Body.WriteAsync(contentBytes, 0, contentBytes.Length);
            }

            return Task.CompletedTask;
        }
    }
}
