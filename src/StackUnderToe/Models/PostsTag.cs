// This class was automatically generated with love by ST4bby 9/25/2012 10:08:44 PM.
// Read more at http://jbubriski.github.com/ST4bby/

namespace StackUnderToe
{
	using System;
	using System.ComponentModel.DataAnnotations;
	
	public class PostsTag
	{
		[Required, Display(Name="Post Id")]
		public int PostId { get; set; }
		
		[Required, Display(Name="Tag"), StringLength(100)]
		public string Tag { get; set; }
	}
}
