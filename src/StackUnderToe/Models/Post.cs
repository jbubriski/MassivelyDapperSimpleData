// This class was automatically generated with love by ST4bby 9/25/2012 10:08:44 PM.
// Read more at http://jbubriski.github.com/ST4bby/

namespace StackUnderToe
{
	using System;
	using System.ComponentModel.DataAnnotations;
	
	public class Post
	{
		[Required, Display(Name="Id")]
		public int Id { get; set; }
		
		[Display(Name="Post Type Id")]
		public Nullable<int> PostTypeId { get; set; }
		
		[Display(Name="Accepted Answer Id")]
		public Nullable<int> AcceptedAnswerId { get; set; }
		
		[Display(Name="Creation Date")]
		public Nullable<DateTime> CreationDate { get; set; }
		
		[Display(Name="Score")]
		public Nullable<int> Score { get; set; }
		
		[Display(Name="View Count")]
		public Nullable<int> ViewCount { get; set; }
		
		[Display(Name="Body")]
		public string Body { get; set; }
		
		[Display(Name="Owner User Id")]
		public Nullable<int> OwnerUserId { get; set; }
		
		[Display(Name="Owner Display Name"), StringLength(80)]
		public string OwnerDisplayName { get; set; }
		
		[Display(Name="Last Editor User Id")]
		public Nullable<int> LastEditorUserId { get; set; }
		
		[Display(Name="Last Edit Date")]
		public Nullable<DateTime> LastEditDate { get; set; }
		
		[Display(Name="Last Activity Date")]
		public Nullable<DateTime> LastActivityDate { get; set; }
		
		[Display(Name="Title"), StringLength(500)]
		public string Title { get; set; }
		
		[Display(Name="Tags"), StringLength(300)]
		public string Tags { get; set; }
		
		[Display(Name="Answer Count")]
		public Nullable<int> AnswerCount { get; set; }
		
		[Display(Name="Comment Count")]
		public Nullable<int> CommentCount { get; set; }
		
		[Display(Name="Favorite Count")]
		public Nullable<int> FavoriteCount { get; set; }
		
		[Display(Name="Closed Date")]
		public Nullable<DateTime> ClosedDate { get; set; }
		
		[Display(Name="Parent Id")]
		public Nullable<int> ParentId { get; set; }
    }
}
