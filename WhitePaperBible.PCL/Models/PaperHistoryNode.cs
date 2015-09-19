using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	[DataContract(Name = "popular")]
	public class PaperHistoryNode
	{
		[DataMember(Name = "paper_history")]
		public History history { get; set; }
	}
}
