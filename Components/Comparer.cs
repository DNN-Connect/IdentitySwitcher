// 
// Copyright (c) 2018 DNN Connect, https://www.dnn-connect.org
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this 
// software and associated documentation files (the "Software"), to deal in the Software 
// without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons 
// to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//
// VBConversions Note: VB project level imports

using System.Collections;
// End of VB project level imports

using DotNetNuke.Entities.Users;



namespace DNN.Modules.IdentitySwitcher
{
	
	public enum SortBy
	{
		DisplayName,
		UserName
	}
	
	public class Comparer : IComparer
	{
		
		private SortBy sortedBy = SortBy.UserName;
		
		public Comparer(SortBy sortby)
		{
			sortedBy = sortby;
		}
		
		public int Compare(object x, object y)
		{
			
			UserInfo u1 = (UserInfo) x;
			UserInfo u2 = (UserInfo) y;
			
			switch (sortedBy)
			{
				case SortBy.DisplayName:
					return new CaseInsensitiveComparer().Compare(u1.DisplayName, u2.DisplayName);
				case SortBy.UserName:
					return new CaseInsensitiveComparer().Compare(u1.Username, u2.Username);
			}
			
			return 0;
		}
		
	}
	
}
