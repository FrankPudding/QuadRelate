using Microsoft.WindowsAzure.Storage.Table;

namespace QuadRelate.Cloud
{
    public class ResultEntity: TableEntity
    {
        public string Board { get; set; }
        public string Winner { get; set; }
    }
}