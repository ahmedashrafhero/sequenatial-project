using System;
using System.Collections.Generic;
using System.Text;

namespace seqMaze
{
    class operations
    {
        public static int ctr = -1;
        public static List<List<int[,]>> paths = new List<List<int[,]>>();
        public matrixElement[,] Create_matrix(int row, int column, List<int[]> blocks, int[] start_element, int[] end_element)//بكريت الماتريكس بناء على الانبوت اللى داخلها
        {
            matrixElement[,] matrix = new matrixElement[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = new matrixElement();
                }
            }
            for (int k = 0; k < blocks.Count; k++)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (i == blocks[k][0] && j == blocks[k][1])
                        {
                            matrix[i, j].element_status = false;
                        }
                    }
                }
            }
            matrix[start_element[0], start_element[1]].element_type = 1;
            matrix[end_element[0], end_element[1]].element_type = 2;
            return matrix;
        }
        public void finding_path(matrixElement[,] matrix, int[] start_position, int[] parent_position, int[] matrix_size)// مهمتها انها ترجعلى الباث السليم فى ليست
        {
            for (int i = 0; i < matrix_size[0]; i++)
            {
                for (int j = 0; j < matrix_size[1]; j++)
                {
                    //بندور على الstrart position
                    if (i == start_position[0] && j == start_position[1])
                    {
                        if (matrix[i, j].visited == true && matrix[parent_position[0], parent_position[1]].visited == true)//لو الايليمنت دا والايلمينت الاب بتاعه اتزارو قبل كدا اخرج من الريكرجن دى لانها انت كدا دخلت فى لووب مبتخلصش
                        {
                            matrix[i, j].visited = false;
                            return;
                        }
                        matrix[parent_position[0], parent_position[1]].visited = true;
                        if (matrix[i, j].element_type == 2)//لو وصلت للجول
                        {
                            matrix[parent_position[0], parent_position[1]].goal_flag = true;//ضفلى الاب بتاعك مع الجول برده عشان اضيفه فى الليست
                            ctr++;
                            paths.Add(new List<int[,]>());
                            paths[ctr].Add(new[,] { { i, j } });//ال3 سطور دول مجرد بعمل اد للايليمنت دا فى الليست بس مش اكتر
                            return;
                        }
                        if (matrix[i, j].direction == 0)//لو الرعبية بصه لفوق
                        {
                            if (j + 1 < matrix_size[1])//بتشيك انى لو هخش يمين فانا مش على طرف الماتريكس خالص لسة
                            {
                                if (matrix[i, j + 1].element_status != false)//بتشيك انى لو هخش يمين فاليمين دا مش مقفول معملو بلوك يعنى
                                {
                                    matrix[i, j + 1].direction = 1;
                                    finding_path(matrix, new[] { i, j + 1 }, new[] { i, j }, matrix_size);
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)//لو رجعت من الفانكشن اللى نادتها فوق دى بالجول فساعتها مش هحتاج اكمل ولازم ارجع لغاية الاستارت ايليمنت تانى واجمع كل الايليمنتس اللى هرجع بيها دى فى الليست  
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)//لو انا راجع من الجول برده بس فى الاستارت ايليمنت دلوقتى فساعتها هضيفها فى الليست بس مش هخرج لا هكمل من اول وجديد عادى بقا عشان اشوف لو هعرف اجيب باثز اصغر لسة
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                            if (i - 1 >= 0)//هنا بقا بتشيك عشان اطلع فوق بقا وبعمل نفس الخطوات بالظبط بس عشان اطلع فوق بقا
                            {
                                if (matrix[i - 1, j].element_status != false)
                                {
                                    matrix[i - 1, j].direction = 0;
                                    finding_path(matrix, new[] { i - 1, j }, new[] { i, j }, matrix_size);
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)//هنا بتشيك تانى برده نفس الكلام بالظبط لو صولت للجول اخرج ما عدا الاستارت وهكذا
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                        }
                        //هكرر نفس الكلام دا لو العربية بصة ناحية الشرق او الغرب او الجنوب برده بالظبط
                        if (matrix[i, j].direction == 1)//east
                        {
                            if (i + 1 < matrix_size[0])
                            {
                                if (matrix[i + 1, j].element_status != false)
                                {
                                    matrix[i + 1, j].direction = 2;
                                    finding_path(matrix, new[] { i + 1, j }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                            if (j + 1 < matrix_size[1])
                            {
                                if (matrix[i, j + 1].element_status != false)
                                {
                                    matrix[i, j + 1].direction = 1;
                                    finding_path(matrix, new[] { i, j + 1 }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                        }
                        if (matrix[i, j].direction == 2)//south
                        {
                            if (j - 1 >= 0)
                            {
                                if (matrix[i, j - 1].element_status != false)
                                {
                                    matrix[i, j - 1].direction = 3;
                                    finding_path(matrix, new[] { i, j - 1 }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                            if (i + 1 < matrix_size[0])
                            {
                                if (matrix[i + 1, j].element_status != false)
                                {
                                    matrix[i + 1, j].direction = 2;
                                    finding_path(matrix, new[] { i + 1, j }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                        }
                        if (matrix[i, j].direction == 3)//west
                        {
                            if (j - 1 >= 0)
                            {
                                if (matrix[i, j - 1].element_status != false)
                                {
                                    matrix[i, j - 1].direction = 3;
                                    finding_path(matrix, new[] { i, j - 1 }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                            if (i - 1 >= 0)
                            {
                                if (matrix[i - 1, j].element_status != false)
                                {
                                    matrix[i - 1, j].direction = 0;
                                    finding_path(matrix, new[] { i - 1, j }, new[] { i, j }, matrix_size);
                                    matrix[i, j].visited = true;
                                }
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type != 1)
                            {
                                matrix[parent_position[0], parent_position[1]].goal_flag = true;
                                paths[ctr].Add(new[,] { { i, j } });
                                return;
                            }
                            if (matrix[i, j].goal_flag == true && matrix[i, j].element_type == 1)
                            {
                                paths[ctr].Add(new[,] { { i, j } });
                                ctr = -1;
                            }
                        }

                        matrix[i, j].visited = false;
                    }
                }
            }

        }


        public List<int[,]> smallest_list()//الفانكشن دى بتشوف اصغر ليست فى مجموعة الليستسز اللى طلعنا بيها من الفانكشن اللى فاتت وترجعها لانها هتبقى كدا اصغر باث يعنى 
        {
            List<int[,]> array = paths[0];
            for (int i = 1; i < paths.Count; i++)
            {
                if (paths[i].Count < array.Count)
                {
                    array = paths[i];
                }
            }
            foreach (int[,] item in array)
            {
                Console.WriteLine("{0}{1}", item[0, 0], item[0, 1]);
            }
            return array;
        }
    }
}
