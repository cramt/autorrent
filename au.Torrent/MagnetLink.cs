using Leak.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace au.Torrent {
    public class MagnetLink {
        public static MagnetLink Parse(string magnetLink) {
            const string firstPart = "magnet:?xt=urn:btih:";
            if(magnetLink.Substring(0, firstPart.Length) != firstPart) {
                throw new FormatException("the first part of the magnet link doesnt start with " + firstPart);
            }
            string[] args = WebUtility.UrlDecode(magnetLink.Substring(firstPart.Length)).Split('&');
            string hash = null;
            string name = null;
            List<string> trackers = new List<string>();
            args.ToList().ForEach(x => {
                string[] splitet = x.Split('=');
                switch (splitet.Length) {
                    case 1:
                        hash = splitet[0];
                        break;
                    case 2:
                        switch (splitet[0]) {
                            case "dn":
                                if(name == null) {
                                    name = splitet[1];
                                }
                                else {
                                    throw new Exception("multible names where provided");
                                }
                                break;
                            case "tr":
                                trackers.Add(splitet[1]);
                                break;
                        }
                        break;
                    default:
                        throw new FormatException("formatting error, involing the '=' char");
                }
            });
            return new MagnetLink() {
                Hash = FileHash.Parse(hash),
                Trackers = trackers.ToArray(),
                Name = name
            };
        }
        public string[] Trackers = null;
        public FileHash Hash = null;
        public string Name = null;
    }
}
