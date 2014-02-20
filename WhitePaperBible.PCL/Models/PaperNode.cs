namespace WhitePaperBible.Core.Models
{
	public class PaperNode
	{
		public Paper paper { get; set; }
		
		public string Index {
			get {
				return paper.title.Length == 0 ? "A" : paper.title [0].ToString ().ToUpper ();
			}
		}
	}
}
