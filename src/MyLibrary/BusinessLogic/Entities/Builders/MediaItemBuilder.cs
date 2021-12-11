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

        private MediaItemBuilder(string title, ItemType type, long number, int releaseYear)
        {
            this.item = new MediaItem
            { 
                Title = title, 
                Type = type,
                Number = number,
                ReleaseYear = releaseYear
            };
        }

        private MediaItemBuilder(int id, string title, ItemType type, long number, int releaseYear)
            : this(title, type, number, releaseYear)
        {
            this.item.Id = id;
        }

        public static MediaItemBuilder CreateCd(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.Cd, number, releaseYear);     
        }

        public static MediaItemBuilder CreateDvd(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.Dvd, number, releaseYear);
        }

        public static MediaItemBuilder CreateBluray(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.BluRay, number, releaseYear);
        }

        public static MediaItemBuilder CreateVhs(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.Vhs, number, releaseYear);
        }

        public static MediaItemBuilder CreateVinyl(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.Vinyl, number, releaseYear);
        }

        public static MediaItemBuilder CreateMiscMediaItem(string title, long number, int releaseYear)
        {
            return new MediaItemBuilder(title, ItemType.Other, number, releaseYear);
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
