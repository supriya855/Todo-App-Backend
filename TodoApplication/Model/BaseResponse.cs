namespace TodoApplicationWebAPI.Model
{
    public class BaseResponse<T>
    {
        public string? ResponseCode { get; set; }

        public string? ResponseMessage { get; set; }

        public string? ResultMessage { get; set; }
        public T? ResultsetData { get; set; }

    }
    //public class BaseResponse
    //{
    //    public string? ResponseCode { get; set; }

    //    public string? ResponseMessage { get; set; }

    //    public string? ResultMessage { get; set; }
    //}
}
