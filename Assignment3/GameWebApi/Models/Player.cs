using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models
{
    public class Player
    {
        public Player()
        {
            PlayerItems = new List<Item>();
        }

        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //[BsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> PlayerItems { get; set; }
    }
}
