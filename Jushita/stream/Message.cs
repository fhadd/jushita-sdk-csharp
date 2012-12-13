using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Util;
using System.Collections;


namespace Top.Api.Jushita.stream
{
    public class Message
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long currentTimeMills()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        private string _topic;
        public string topic
        { 
            get { return _topic; }
        }

        private string _pubAppKey;
        public string pubAppKey
        {
            get { return _pubAppKey; }
        }

        private string _offset;
        public string offset
        {
            get { return _offset; }
        }


        private string _nextOffset;
        public string nextOffset
        {
            get { return _nextOffset; }
        }


        private string _md5;
        public string md5
        {
            get { return _md5; }
        }

        private long _userId;
        public long userId
        {
            get { return _userId; }
        }

        private string _message;
        public string message
        {
            get { return _message; }
        }


        private Offset _offsetDO;
        public Offset offsetDO
        {
            get { return _offsetDO; }
        }


        private Offset _nextOffsetDO;
        public Offset nextOffsetDO
        {
            get { return _nextOffsetDO; }
        }


        private int _index;
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }

        private volatile State _state = State.UNKNOWN;
        public State state
        {
            get { return _state; }
            set { _state = value; }
        }

        private long _takeTime;
        public long takeTime
        {
            get { return _takeTime; }
        }

        public void taken()
        {
            this._takeTime = currentTimeMills();
        }

        private long putTime = currentTimeMills();

        public Message(string jsonText)
        {
            IDictionary dict = TopUtils.ParseJson(jsonText);
            this._topic = dict["topic"] as string;
            this._message = dict["message"] as string;
            this._nextOffset = dict["nextOffset"] as string;
            this._offset = dict["offset"] as string;
            this._md5 = dict["md5"] as string;
            this._pubAppKey = dict["pubAppKey"] as string;
            //this._userId = long.Parse(dict["userId"] as string);
            this._userId = Convert.ToInt64(dict["userId"]);
            this._offsetDO = new Offset(_offset);
            this._nextOffsetDO = new Offset(_nextOffset);
        }

        public override string ToString()
        {
            return "JIPMessage{" +
                "topic='" + topic + '\'' +
                ", pubAppKey='" + pubAppKey + '\'' +
                ", offset='" + offset + '\'' +
                ", nextOffset='" + nextOffset + '\'' +
                ", userId=" + userId +
                ", message='" + message + '\'' +
                '}';
        }

        public class Offset
        {
            public long id;
            public string partition;
            public long offset;

            public Offset(long id, string partition, long offset)
            {
                this.id = id;
                this.partition = partition;
                this.offset = offset;
            }

            public Offset(string offsetStr)
            {
                string[] splitResult = offsetStr.Split('@');
                this.id = long.Parse(splitResult[0]);
                this.partition = splitResult[1];
                this.offset = long.Parse(splitResult[2]);
            }
        }


        public enum State
        {
            SUCCESS, FAILED, UNKNOWN
        }
    }
}
