using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arena.Core;

namespace Arena.Custom.Story.Entity
{
    [Serializable]
    public class StoryCollection : ArenaCollectionBase
    {

        #region Class Indexers

		public new Story this[int index]
		{
			get
			{
				if (this.List.Count > 0)
				{
					return (Story)this.List[index];
				}
				else
				{
					return null;
				}
			}
			set
			{
				this.List[index] = value;
			}
		}

		#endregion

		#region Constructors

		public StoryCollection()
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds the LibraryItem to the current list
		/// </summary>
		/// <param name="item"></param>
		public void Add(Story item)
		{
			this.List.Add(item);
		}

		/// <summary>
		/// Inserts the LibraryItem into the current list at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, Story item)
		{
			this.List.Insert(index, item);
		}

		#endregion
    }
}
