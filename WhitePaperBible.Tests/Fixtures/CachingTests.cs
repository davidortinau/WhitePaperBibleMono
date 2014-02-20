using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using WhitePaperBible.Core.Models;
using System.Xml.Serialization;

namespace WhitePaperBible.Tests
{
	[TestFixture]
    public class CachingTests
	{
		/**
		 * Initialization
		 * 1) Detect Network status
		 * 1a) Online - Get Papers and Tags. 
		 * 		- cache
		 * 		- proceed to first view
		 * 1b) Offline - load from cache. If not available show empty
		 * 
		 * 2) Begin loading references on a background thread(?)
		 * 
		 * Pull to refresh updates cache for Papers and Tags
		 * 
		 * Getting references updates local and caches or updates that data
		 * 
		 * Should I get Tags + TagPaper references so we can lookup offline?
		 * 
		 * Start by doing what we do now plus caching.
		 * 
		 * Caching is going to be device specific. Probably should put that stuff in a platform specific project
		 * 
		 * **/
		
		PaperCollection papers;
		
		[SetUp]
		public void init ()
		{
			papers = new PaperCollection ();
			papers.Title = "This is my data model";
			papers.Papers = new List<Paper> ();
			// mock up some papers
		}
		
		[TearDown]
		public void destroy ()
		{
			if (File.Exists ("dataModel.dat")) {
				File.Delete ("dataModel.dat");
				Console.WriteLine ("Hey, we found a file and we tried to delete it");
			}
		}
		
		[Test]
		public void CanCreateAFile ()
		{
			using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication()) {
				using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("dataModel.dat", FileMode.Create, isf)) {
					var serializer = new XmlSerializer (typeof(PaperCollection));
					serializer.Serialize (stream, papers);
				}
			}
			
			// go get the file
			using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication()) {
				using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("dataModel.dat", FileMode.OpenOrCreate, isf)) {
					Assert.True (stream.Length > 0, "should have a dataModel.dat file");
					
					var serializer = new XmlSerializer(typeof(PaperCollection));
					PaperCollection col = serializer.Deserialize(stream) as PaperCollection;
					
					Assert.NotNull( col, "collection should not be null");
					Assert.That(col.Title == papers.Title, "titles should match");
				}
			}
			
			
		}
	}
}
