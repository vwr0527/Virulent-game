using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

/**
 * This is a list of generic objects that are created and die often.
 * To avoid excessive garbage build up, we do not "delete" elements
 * that have died. We simply mark them as deleted. Then, when a new
 * element is spawned, we re-incarnate the element as a new element.
 * Since a type T may or may not have a method that copies all it's
 * members into a new T, We use a Action<> specified by the user to
 * copy a newly spawned object into the list.
 */
namespace Virulent_dev
{
    //remember to reset the size (by clearing the list) after a level
    class RecycleArray<T>
    {
        List<Cell<T>> cellList;
        int max_index = 0;
        int num_active = 0;
        int current_index = 0;
        Action<T, T> CopyMembers;
        bool set_data_instead_of_copy = false;

        public RecycleArray(Action<T,T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>();
        }
        public RecycleArray(int size, Action<T,T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>(size);
            max_index = size - 1;
        }
        public RecycleArray(IEnumerable<Cell<T>> collection, Action<T, T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>(collection);
            max_index = collection.Count() - 1;
        }
        //==================================================

        // if it's full, add to the list.
        // if it's not full, When an empty cell is found,
        // clone the added element into it.
        // afterwards, current index is the newly added element
        public void Add(T data)
        {
            //It's full. extend the list.
            if (num_active == max_index)
            {
                //create and add a new cell to the cell list.
                Cell<T> cell = new Cell<T>();
                cell.SetData(data);
                cell.Activate();
                cellList.Add(cell);
                ++max_index;
                ++num_active;
                current_index = max_index;

                return;
            }

            //it's not full. There are empty cells. Find them and fill them.
            if (num_active < max_index)
            {
                for (int i = 0; i < max_index; ++i)
                {
                    if (cellList[i].IsActive() == false)
                    {
                        current_index = i;
                        if (set_data_instead_of_copy)
                            cellList[i].SetData(data);
                        else
                            cellList[i].CopyData(data, CopyMembers);
                        cellList[i].Activate();
                        ++num_active;

                        return;
                    }
                }
            }
            //if reached here, add failed
            //TODO: Add error
        }

        public void EmptyAll()
        {
            for (int i = 0; i < max_index; ++i)
            {
                cellList[i].Deactivate();
            }
            num_active = 0;
            current_index = 0;
        }

        public void DeleteElementAt(int index)
        {
            cellList[index].Deactivate();
        }

        public void DeleteElement(T data)
        {
            for (int i = 0; i < max_index; ++i)
            {
                if (cellList[i].GetData().Equals(data))
                {
                    cellList[i].Deactivate();
                    return;
                }
            }
        }

        public T ElementAt(int index)
        {
            return cellList[index].GetData();
        }

        public int Size()
        {
            return num_active;
        }

        public int Capacity()
        {
            return max_index;
        }

        public T GetEmptyElement()
        {
            T result = default(T);
            for (int i = 0; i < max_index; ++i)
            {
                if (!cellList[i].IsActive())
                {
                    result = cellList[i].GetData();
                }
            }
            return result;
        }

        //If the data is being stored somewhere else, overwriting the data
        //won't cause garbage collection, because it won't delete the only
        //reference to the old data. It will only be copying references.
        public void SetDataMode(bool activate_set_mode)
        {
            set_data_instead_of_copy = activate_set_mode;
        }

        public void Debug()
        {
            System.Diagnostics.Debug.WriteLine(num_active);
            for (int i = 0; i < max_index; ++i)
            {
                if (cellList[i].IsActive())
                {
                    T result = cellList[i].GetData();
                    System.Diagnostics.Debug.WriteLine(result);
                }
            }
        }
    }
}
