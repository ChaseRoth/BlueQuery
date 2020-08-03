﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace BlueQueryLibrary.Data
{
    public class Tribe
    {
        /// <summary>
        ///     Our true primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Name of the tribe which is unique
        /// </summary>
        public string NameId { get; set; }

        /// <summary>
        ///     Contains discord guilds which have been permitted to access the data located in FolderName variable
        /// </summary>
        public List<GuildInfo> PermittedGuilds { get; set; } = new List<GuildInfo>();

        /// <summary>
        ///     Base folder that contains all information related to this tribe
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        ///     The owner of the tribe - the creator
        /// </summary>
        public ulong Owner { get; set; }

        /// <summary>
        ///     Adds all guildIds to the PermittedGuilds property.<br/>
        ///     If a guildId is already present in the collection its not added.<br/>
        ///     @param - guildIds, ids to be added
        /// </summary>
        /// <param name="guildIds"> Ids to be added </param>
        public void AddGuilds(List<ulong> guildIds)
        {
            foreach (var id in guildIds)
            {
                if (PermittedGuilds.All(p => p.Id != id))
                {
                    PermittedGuilds.Add(new GuildInfo 
                    { 
                        Id = id,
                        DateAdded = DateTime.Now
                    });
                }
            }
        }
    }

    public struct GuildInfo
    {
        public ulong Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
