using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MqttClientTest
{
    [Table(Name = "Message")]
    public class CollectionMessage 
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 采集的消息Id
        /// </summary>
        public long CollectionDataDetailId { get; set; }

        /// <summary>消息编号</summary>
        public string MessageNo { get; set; }

        /// <summary>消息类型</summary>
        public string MessageType { get; set; }

        /// <summary>消息节点ID</summary>
        public string NodeId { get; set; }

        /// <summary>消息文本(只取前50字符)</summary>
        public string MessageText { get; set; }

        /// <summary>消息文件</summary>
        public string MessageFile { get; set; }

        public bool IsRevision { get; set; }

        public string RevisionText { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
