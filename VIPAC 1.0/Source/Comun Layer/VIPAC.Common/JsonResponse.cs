namespace VIPAC.Common
{
    public class JsonResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public int ErrorCode { get; set; }
        public int WarningCode { get; set; }
    }
}