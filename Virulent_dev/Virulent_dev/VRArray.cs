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
    class VRArray<T>
    {
        List<Cell<T>> cellList;
        int max_index = 0;
        int num_active = 0;
        int current_index = 0;
        Action<T, T> CopyMembers;

        public VRArray(Action<T,T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>();
        }
        public VRArray(int size, Action<T,T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>(size);
            max_index = size - 1;
        }
        public VRArray(IEnumerable<Cell<T>> collection, Action<T, T> copyMethod)
        {
            CopyMembers = copyMethod;
            cellList = new List<Cell<T>>(collection);
            max_index = collection.Count() - 1;
        }
        //==================================================

        // if it's full, add to the list.
        // if it's not full, check the position at current_index + 1 and -1,
        // then +2 and -2, then +3 and -3 and so on. When an empty cell is found,
        // clone the added element into it.
        // afterwards, current index is the newly added element
        public void Add(T data)
        {
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
            //===================

            if (num_active < max_index)
            {
                //Debug.WriteLine("======================");
                for (int i = 0; i < max_index; ++i)
                {
                    if (!cellList[i].IsActive())
                    {
                        ///Debug.WriteLine("not active");
                        current_index = i;
                        cellList[i].CopyData(data, CopyMembers);
                        cellList[i].Activate();
                        ++num_active;
                        return;
                    }
                    //else
                        //Debug.WriteLine("active");
                }
            }
        }

        public void EmptyAll()
        {
            for (int i = 0; i < max_index; ++i)
            {
                //Debug.WriteLine("HELLO " + cellList[i].IsActive());
                cellList[i].Deactivate();
                //Debug.WriteLine("GOODBYE " + cellList[i].IsActive());
            }
            num_active = 0;
            current_index = 0;
            //Debug.WriteLine("just emptied all");
            /*
            for (int i = 0; i < max_index; ++i)
            {
                Debug.WriteLine(cellList[i].IsActive());
            }
             */
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
    }

}



/* this is the generic algorithm. I would polish this but it's useless for now
public static void OscillatingSearch()
{
    int j = 0;
    for (int i = 0; i < 10; ++i)
    {
        if (i % 2 == 0)
        {
            ++j;
            int backward = 6 - j;
            if (backward < 0)
            {
                backward += 10;
            }
            Debug.WriteLine(backward);
        }
        else
        {
            int forward = Math.Abs(5 + j);
            Debug.WriteLine(forward);
        }
    }
}
 * 


                int j = 0;
                for (int i = 0; i < max_index; ++i)
                {
                    if (i % 2 == 0)
                    {
                        ++j;
                        int backward = current_index + 1 - j;
                        if (backward < 0)
                        {
                            backward += max_index;
                        }
                        if (cellList.ElementAt(backward).isEmpty)
                        {
                            Reincarnate(data, backward);
                            current_index = backward;
                            ++num_active;
                            return;
                        }
                    }
                    else
                    {
                        int forward = Math.Abs(current_index + j);
                        if (cellList.ElementAt(forward).isEmpty)
                        {
                            Reincarnate(data, forward);
                            current_index = forward;
                            ++num_active;
                            return;
                        }
                    }
                }
*/