﻿using System;
using Newtonsoft.Json.Linq;

namespace XBMC.JsonRpc
{
    public abstract class XbmcMediaPlaylist<TMediaType>
        : XbmcJsonRpcNamespace
        where TMediaType : XbmcPlayable
    {
        #region Private variables

        private string playlistName;

        #endregion

        #region Constructor

        protected XbmcMediaPlaylist(string playlistName, JsonRpcClient client)
            : base(client)
        {
            if (string.IsNullOrEmpty(playlistName))
            {
                throw new ArgumentException();
            }

            this.playlistName = playlistName;
        }

        #endregion

        #region JSON RPC Call

        public virtual bool Play()
        {
            this.client.LogMessage("XbmcMediaPlaylist.Play()");

            return (this.client.Call(this.playlistName + ".Play") != null);
        }

        public virtual bool Play(int itemIndex)
        {
            this.client.LogMessage("XbmcMediaPlaylist.(" + itemIndex + ")");

            return (this.client.Call(this.playlistName + ".Play", itemIndex) != null);
        }

        public virtual bool SkipPrevious()
        {
            this.client.LogMessage("XbmcMediaPlaylist.SkipPrevious()");

            return (this.client.Call(this.playlistName + ".SkipPrevious") != null);
        }

        public virtual bool SkipNext()
        {
            this.client.LogMessage("XbmcMediaPlaylist.SkipNext()");

            return (this.client.Call(this.playlistName + ".SkipNext") != null);
        }

        public abstract TMediaType GetCurrentItem(params string[] fields);

        public abstract XbmcPlaylist<TMediaType> GetItems(params string[] fields);

        public abstract XbmcPlaylist<TMediaType> GetItems(int start, int end, params string[] fields);

        public virtual bool Add(string file)
        {
            this.client.LogMessage("XbmcMediaPlaylist.Add(" + file + ")");

            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentException("file");
            }

            JObject args = new JObject();
            args.Add(new JProperty("file", file));

            return (this.client.Call(this.playlistName + ".Add", args) != null);
        }

        public virtual bool Clear()
        {
            this.client.LogMessage("XbmcMediaPlaylist.Clear()");

            return (this.client.Call(this.playlistName + ".Clear") != null);
        }

        public virtual bool Shuffle()
        {
            this.client.LogMessage("XbmcMediaPlaylist.Shuffle()");

            return (this.client.Call(this.playlistName + ".Shuffle") != null);
        }

        public virtual bool UnShuffle()
        {
            this.client.LogMessage("XbmcMediaPlaylist.UnShuffle()");

            return (this.client.Call(this.playlistName + ".UnShuffle") != null);
        }

        #endregion

        #region Helper functions

        protected JObject getItems(string[] fields, string[] defaultFields, int start, int end) 
        {
            JObject args = new JObject();
            if (fields != null && fields.Length > 0)
            {
                args.Add(new JProperty("fields", fields));
            }
            else
            {
                args.Add(new JProperty("fields", defaultFields));
            }
            if (start >= 0)
            {
                args.Add(new JProperty("start", start));
            }
            if (end >= 0)
            {
                args.Add(new JProperty("end", end));
            }

            return (this.client.Call(this.playlistName + ".GetItems", args) as JObject);
        }

        #endregion
    }
}
