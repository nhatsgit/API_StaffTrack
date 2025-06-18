namespace API_StaffTrack.Models.Common
{
    public class PagingRequestBase
    {
        public int Page { get; set; } = 1;
        public int Record { get; set; } = 10;
    }
}
