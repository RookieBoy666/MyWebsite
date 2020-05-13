 

namespace Utilities
{
    public class ReqMsg
    {
        public bool Success { get; set; }

        public string ErrorMsg { get; set; }

        public ReqMsg() { }

        public ReqMsg(bool success, string errorMsg)
        {
            this.Success = success;
            this.ErrorMsg = errorMsg;
        }

        public override string ToString()
        {
            return ConvertJson.ToJson(this);
        }
    }
}
