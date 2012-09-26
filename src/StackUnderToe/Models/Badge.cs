// This class was automatically generated with love by ST4bby 9/25/2012 10:08:44 PM.
// Read more at http://jbubriski.github.com/ST4bby/

namespace StackUnderToe
{
	using System;
	using System.ComponentModel.DataAnnotations;
	
	public class Badge
	{
		[Key, Required, Display(Name="Id")]
		public int Id { get; set; }
		
		[Display(Name="User Id")]
		public Nullable<int> UserId { get; set; }
		
		[Display(Name="Name"), StringLength(100)]
		public string Name { get; set; }
		
		[Display(Name="Creation Date")]
		public Nullable<DateTime> CreationDate { get; set; }
	}
}
