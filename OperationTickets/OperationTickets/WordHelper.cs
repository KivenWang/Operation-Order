using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using log4net;
namespace OperationTickets
{
    public class WordHelper
    {
        private static readonly ILog logger = LogManager.GetLogger("Main");
        /// <summary>
        /// 模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">新文件</param>
        /// <param name="myDictionary">参数数组</param>
        /// <param name="startPageIndex">word页数</param>
        public static bool ExportWord(string templateFile, string fileName, Dictionary<string, string> myDictionary, int startPageIndex)
        {
            //生成documnet对象
            Word._Document doc = new Word.Document();
            //生成word程序对象
            Word.Application app = new Word.Application();
            //模板文件
            string TemplateFile = templateFile;
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, fileName);

            object Obj_FileName = fileName;
            object Visible = false;
            object ReadOnly = false;
            object missing = System.Reflection.Missing.Value;

            try
            {
                //打开文件
                doc = app.Documents.Open(ref Obj_FileName, ref missing, ref ReadOnly, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref Visible,
                    ref missing, ref missing, ref missing,
                    ref missing);
                doc.Activate();

                #region 声明参数
                if (myDictionary.Count > 0)
                {
                    object what = Word.WdGoToItem.wdGoToBookmark;
                    object WordMarkName;
                    foreach (var item in myDictionary)
                    {
                        WordMarkName = item.Key;
                        //光标转到书签的位置
                        doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref WordMarkName);
                        //插入的内容，插入位置是word模板中书签定位的位置
                        doc.ActiveWindow.Selection.TypeText(item.Value);
                        //设置当前定位书签位置插入内容的格式
                        //doc.ActiveWindow.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                }
                #endregion
                
                #region 删除指定页面
                //获取页数
                int pages = doc.ComputeStatistics(Word.WdStatistic.wdStatisticPages, ref missing);
                //超出3页的部分
                startPageIndex += pages - 3;
                //共3页,少于3页时删除多余页
                if (startPageIndex < pages )
                {
                    object objWhat = Word.WdGoToItem.wdGoToPage;

                    object objWhich = Word.WdGoToDirection.wdGoToAbsolute;
                    //删除startPageIndex页及之后的文档
                    object objPage = startPageIndex + 1;

                    Word.Range rangeStart = doc.GoTo(ref objWhat, ref objWhich, ref objPage, ref missing);

                    object objStart = rangeStart.Start - 1;
                    object objEnd = doc.Characters.Count;

                    object Unit = (int)Word.WdUnits.wdCharacter;
                    object Count = 1;
                    doc.Range(ref objStart, ref  objEnd).Delete(ref  Unit, ref  Count);
                }
                #endregion                
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("WordHelper中发生异常:", ex);
                return false;
            }
            finally
            {
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
            }

        }
    }
}