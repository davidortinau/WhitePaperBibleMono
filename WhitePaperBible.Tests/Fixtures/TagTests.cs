using System;
using NUnit.Framework;
using WhitePaperBibleCore.Services;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;

namespace WhitePaperBible.Tests
{
	[TestFixture]
    public class TagTests
	{
		TagService svc;
		
		[SetUp]
		public void init()
		{
			svc = new TagService();
		}
		
		[Test]
		public void CanUseTagService ()
		{
			Assert.NotNull (svc, "TagService should not be null");
		}

		[Test]
		public void CanGetTags ()
		{
			svc.GetTags(onTagsReceived, onTagsError);
		}
		
		public void onTagsReceived( List<TagNode> tags){
			Assert.NotNull(tags, "Tags list should not be null");
		}
		
		public void onTagsError(string errorMessage){
			Assert.Fail(errorMessage);
		}

//		[Test]
//		public void CanGetPapersByTag ()
//		{
//			svc.GetPapersByTag('love', onTagPapersReceived, onTagPapersError);
//		}
	}
}
