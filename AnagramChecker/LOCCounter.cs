using System;
using System.Collections.Generic;

namespace AnagramChecker
{
    public interface ILocCounter
    {
        int CountLines(IEnumerable<string> codeFilePath);
    }
    public class LocCounter : ILocCounter
    {
        private const string LineComment = "//";
        private const string CommentBlkStart = "/*";
        private const string CommentBlkEnd = "*/";
        private bool _blkComment;
        public int CountLines(IEnumerable<string> code)
        {
            int count = 0;

            foreach (var line in code)
            {
                if (!_blkComment)
                {
                    if (!string.IsNullOrEmpty(line) && IsValidCode(line.Trim()))
                        count++;
                    if (line != null && IsCommentBlockStart(line.Trim()))
                        _blkComment = true;
                }
                else
                {
                    if (IsCommentBlockEnd(line.Trim()))
                        _blkComment = false;
                }
            }
            return count;            
        }

        // Validates the given code line and returns 1 if valid, 0 otherwise. 
        private static bool IsValidCode(string codeLine)
        {
            if (IsLineComment(codeLine))
                return false;
            if (IsBlockComment(codeLine))
                return false;
            return true;
        }

        private static bool IsLineComment(string codeLine)
        {
            return codeLine.StartsWith(LineComment);
        }

        private static bool IsBlockComment(string codeLine)
        {
            if (!codeLine.StartsWith(CommentBlkStart))
                return false;
            var commentBlkEndPosition = codeLine.IndexOf(CommentBlkEnd, StringComparison.Ordinal);
            if (commentBlkEndPosition == -1)
                return true;
            codeLine = codeLine.Substring(commentBlkEndPosition + CommentBlkEnd.Length);
            if (string.IsNullOrEmpty(codeLine))
                return true;
            return IsBlockComment(codeLine);
        }

        private static bool IsCommentBlockStart(string codeLine)
        {
            return IsBlockComment(codeLine) && codeLine.LastIndexOf(CommentBlkStart, StringComparison.Ordinal) > codeLine.LastIndexOf(CommentBlkEnd, StringComparison.Ordinal);
        }

        private static bool IsCommentBlockEnd(string codeLine)
        {
            return codeLine.LastIndexOf(CommentBlkEnd, StringComparison.Ordinal) > codeLine.LastIndexOf(CommentBlkStart, StringComparison.Ordinal);
        }
    }
}