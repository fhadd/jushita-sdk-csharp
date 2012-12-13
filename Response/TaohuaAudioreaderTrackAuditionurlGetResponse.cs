using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// TaohuaAudioreaderTrackAuditionurlGetResponse.
    /// </summary>
    public class TaohuaAudioreaderTrackAuditionurlGetResponse : TopResponse
    {
        /// <summary>
        /// 有声读物单曲试听地址
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
