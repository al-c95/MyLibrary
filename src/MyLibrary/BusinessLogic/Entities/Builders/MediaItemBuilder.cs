using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities.Builders
{
    public sealed class MediaItemBuilder
    {
        private MediaItem item;

        private MediaItemBuilder(string title, ItemType type)
        {
            this.item = new MediaItem { Title = title, Type = type };
        }

        private MediaItemBuilder(int id, string title, ItemType type)
            : this(title, type)
        {
            this.item.Id = id;
        }

        public static MediaItemBuilder CreateCd(string title)
        {
            return new MediaItemBuilder(title, ItemType.Cd);
        }

        public static MediaItemBuilder CreateDvd(string title)
        {
            return new MediaItemBuilder(title, ItemType.Dvd);
        }

        public static MediaItemBuilder CreateBluray(string title)
        {
            return new MediaItemBuilder(title, ItemType.BluRay);
        }

        public static MediaItemBuilder CreateVhs(string title)
        {
            return new MediaItemBuilder(title, ItemType.Vhs);
        }

        public static MediaItemBuilder CreateVinyl(string title)
        {
            return new MediaItemBuilder(title, ItemType.Vinyl);
        }

        public static MediaItemBuilder CreateMiscMediaItem(string title)
        {
            return new MediaItemBuilder(title, ItemType.Other);
        }

        public MediaItemBuilder Numbered(long number)
        {
            this.item.Number = number;
            return this;
        }

        public MediaItemBuilder RunningForMins(int runningTime)
        {
            this.item.RunningTime = runningTime;
            return this;
        }

        public MediaItemBuilder ReleasedInYear(int year)
        {
            this.item.ReleaseYear = year;
            return this;
        }

        public MediaItemBuilder WithTags(IEnumerable<Tag> tags)
        {
            foreach (var t in tags)
                this.item.Tags.Add(t);

            return this;
        }

        public MediaItem Get() => this.item;
    }//class
}
