// This class was automatically generated with love by ST4bby 9/25/2012 10:08:44 PM.
// Read more at http://jbubriski.github.com/ST4bby/

namespace StackUnderToe
{
	using System;
	using System.ComponentModel.DataAnnotations;
	
	public class Vote
	{
		[Required, Display(Name="Id")]
		public int Id { get; set; }
		
		[Display(Name="Post Id")]
		public Nullable<int> PostId { get; set; }
		
		[Display(Name="Vote Type Id")]
		public Nullable<int> VoteTypeId { get; set; }
		
		[Display(Name="Creation Date")]
		public Nullable<DateTime> CreationDate { get; set; }
		
		[Display(Name="User Id")]
		public Nullable<int> UserId { get; set; }
	}
}
