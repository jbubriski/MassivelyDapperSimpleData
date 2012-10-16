// This class was automatically generated with love by ST4bby 9/25/2012 10:08:44 PM.
// Read more at http://jbubriski.github.com/ST4bby/

namespace StackUnderToe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserWithBadges
    {
        [Required, Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Reputation")]
        public Nullable<int> Reputation { get; set; }

        [Display(Name = "Creation Date")]
        public Nullable<DateTime> CreationDate { get; set; }

        [Display(Name = "Display Name"), StringLength(80)]
        public string DisplayName { get; set; }

        [Display(Name = "Last Access Date")]
        public Nullable<DateTime> LastAccessDate { get; set; }

        [Display(Name = "Website Url"), StringLength(400)]
        public string WebsiteUrl { get; set; }

        [Display(Name = "Location"), StringLength(200)]
        public string Location { get; set; }

        [Display(Name = "Age")]
        public Nullable<int> Age { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Views")]
        public Nullable<int> Views { get; set; }

        [Display(Name = "Up Votes")]
        public Nullable<int> UpVotes { get; set; }

        [Display(Name = "Down Votes")]
        public Nullable<int> DownVotes { get; set; }

        public Dictionary<string, int> Badges { get; set; }

        public UserWithBadges()
        {
            Badges = new Dictionary<string, int>();
        }
    }
}
