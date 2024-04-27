using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Globalization;
using System.Runtime.Serialization;

namespace TodoApplicationWebAPI.Model
{
    public class TodoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string uniqueId { get; set; }
            public string Task { get; set; }
            public string status { get; set; }
    }
}
