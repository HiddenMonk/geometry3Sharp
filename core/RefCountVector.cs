﻿using System;

namespace g3
{
    public class RefCountVector
	{
        public static readonly int invalid = -1;


        DVector<short> ref_counts;
        DVector<int> free_indices;
        int used_count;

        public RefCountVector()
        {
            ref_counts = new DVector<short>();
            free_indices = new DVector<int>();
            used_count = 0;
        }


        public bool empty {
            get { return used_count == 0; }
        }
        public int count {
            get { return used_count; }
        }
        public int max_index {
            get { return ref_counts.size; }
        }
        public int is_dense {
            get { return free_indices.Length == 0; }
        }


        public bool isValid(int index) {
            return ( index < ref_counts.size && ref_counts[index] > 0 );
        }


        public int allocate() {
            used_count++;
            if (free_indices.empty) {
                ref_counts.push_back(1);
                return ref_counts.size - 1;
            } else {
                int iFree = free_indices.back;
                free_indices.pop_back();
                ref_counts[iFree] = 1;
                return iFree;
            }
        }



        public int increment(int index, int increment = 1) {
            Util.gDevAssert( isValid(index)  );
            ref_counts[index] += increment;
            return ref_counts[index];       
        }

        public void decrement(int index, int decrement) {
            Util.gDevAssert( isValid(index) );
            ref_counts[index] -= decrement;
            Util.gDevAssert(ref_counts[index] >= 0);
            if (ref_counts[index] == 0) {
                free_indices.push_back(index);
                ref_counts[index] = invalid;
                used_count--;
            }
        }

	}
}
