        public static byte[] GetResetUnstagedLinesAsPatch([NotNull] GitModule module, [NotNull] string text, int selectionPosition, int selectionLength, [NotNull] Encoding fileContentEncoding)
            string body = ToResetUnstagedLinesPatch(selectedChunks);
        public static byte[] GetSelectedLinesAsPatch([NotNull] string text, int selectionPosition, int selectionLength, bool staged, [NotNull] Encoding fileContentEncoding, bool isNewFile)
            string body = ToStagePatch(selectedChunks, staged, isWholeFile: false);
            string[] headerLines = header.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
                    sb.Append(pppLine + "\n");
                    sb.Append(line + "\n");
        public static byte[] GetSelectedLinesAsNewPatch([NotNull] GitModule module, [NotNull] string newFileName, [NotNull] string text, int selectionPosition, int selectionLength, [NotNull] Encoding fileContentEncoding, bool reset, byte[] filePreabmle)
            StringBuilder sb = new StringBuilder();
            const string fileMode = "100000"; // given fake mode to satisfy patch format, git will override this
            sb.Append(string.Format("diff --git a/{0} b/{0}", newFileName));
            sb.Append("\n");
            if (!reset)
                sb.Append("new file mode " + fileMode);
                sb.Append("\n");
            sb.Append("index 0000000..0000000");
            sb.Append("\n");
            if (reset)
                sb.Append("--- a/" + newFileName);
            }
            else
            {
                sb.Append("--- /dev/null");
            sb.Append("\n");
            sb.Append("+++ b/" + newFileName);
            sb.Append("\n");
            string header = sb.ToString();
            var selectedChunks = FromNewFile(module, text, selectionPosition, selectionLength, reset, filePreabmle, fileContentEncoding);

            string body = ToStagePatch(selectedChunks, staged: false, isWholeFile: true);

            // git apply has problem with dealing with autocrlf
            // I noticed that patch applies when '\r' chars are removed from patch if autocrlf is set to true
            if (reset && body != null && module.EffectiveConfigFile.core.autocrlf.ValueOrDefault == AutoCRLFType.@true)
                body = body.Replace("\r", "");
            if (header == null || body == null)
                return null;
                return GetPatchBytes(header, body, fileContentEncoding);
                // if selection intersects with chunsk
        private static IReadOnlyList<Chunk> FromNewFile([NotNull] GitModule module, [NotNull] string text, int selectionPosition, int selectionLength, bool reset, [NotNull] byte[] filePreabmle, [NotNull] Encoding fileContentEncoding)
            return new List<Chunk>
            {
                Chunk.FromNewFile(module, text, selectionPosition, selectionLength, reset, filePreabmle, fileContentEncoding)
            };
        private static string ToResetUnstagedLinesPatch([NotNull, ItemNotNull] IEnumerable<Chunk> chunks)
                return subChunk.ToResetUnstagedLinesPatch(ref addedCount, ref removedCount, ref wereSelectedLines);
        private static string ToStagePatch([NotNull, ItemNotNull] IEnumerable<Chunk> chunks, bool staged, bool isWholeFile)
                return subChunk.ToStagePatch(ref addedCount, ref removedCount, ref wereSelectedLines, staged, isWholeFile);
        public string ToStagePatch(ref int addedCount, ref int removedCount, ref bool wereSelectedLines, bool staged, bool isWholeFile)
            bool selectedLastLine = false;
            for (int i = 0; i < RemovedLines.Count; i++)
                PatchLine removedLine = RemovedLines[i];
                selectedLastLine = removedLine.Selected;
                else if (!staged)
            bool selectedLastRemovedLine = selectedLastLine;

            for (int i = 0; i < AddedLines.Count; i++)
                PatchLine addedLine = AddedLines[i];
                selectedLastLine = addedLine.Selected;
                else if (staged)
            if (PostContext.Count == 0 && (!staged || selectedLastRemovedLine))
            if (PostContext.Count == 0 && (selectedLastLine || staged || isWholeFile))
        public string ToResetUnstagedLinesPatch(ref int addedCount, ref int removedCount, ref bool wereSelectedLines)
            Chunk result = new Chunk();
                    // do not refactor, there are no break points condition in VS Experss
        public static Chunk FromNewFile([NotNull] GitModule module, [NotNull] string fileText, int selectionPosition, int selectionLength, bool reset, [NotNull] byte[] filePreabmle, [NotNull] Encoding fileContentEncoding)
            Chunk result = new Chunk { _startLine = 0 };
                string preamble = i == 0 ? new string(fileContentEncoding.GetChars(filePreabmle)) : string.Empty;