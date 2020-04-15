using MitieNetCore.Wrappers;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class MarshaledWrapperTest
    {
        // A sample text file to analyze
        private const string SAMPLE_TEXT_FILE_NAME = @"..\..\..\..\..\sample1.txt";

        // A Mitie trained sample ner model
        private const string SAMPLE_MITIE_NER_MODEL = @"..\..\..\..\..\ner_model.dat";

        /// <summary>
        /// Loads a sample file into memory
        /// </summary>
        [Fact]
        public void LoadEntireFile()
        {
            // Load the sample text to analyze
            IntPtr ptr = new IntPtr();
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(SAMPLE_TEXT_FILE_NAME,
                                                                        ref ptr);

            Assert.NotNull((object)ptr); // Casted to avoid warning
        }

        /// <summary>
        /// Load a sample text file and get the text tokens.
        /// 
        /// </summary>
        [Fact]
        public void GetTokens()
        {
            // Load the sample text to analyze
            // Default overload used here is to release unmanaged memory
            IntPtr ptr = new IntPtr();
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(SAMPLE_TEXT_FILE_NAME,
                                                                        ref ptr);

            IntPtr tokens = new IntPtr();
            
            var tokenList = MarshaledWrapper.Exec_mitie_tokenize(sentence, ref tokens, true);

            Assert.True(tokenList.Count == 212);
        }

        /// <summary>
        /// Load a sample text file and get the otekens offsets
        /// </summary>
        [Fact]
        public void GetTokenOffsets()
        {
            // Load the sample text to analyze
            IntPtr ptr = new IntPtr();
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(SAMPLE_TEXT_FILE_NAME,
                                                                        ref ptr);

            IntPtr tokens = new IntPtr();
            IntPtr offsetTokens = new IntPtr();
            List<uint> listOffsetTokens = new List<uint>();

            // Tokenize with offsets
            MarshaledWrapper.Exec_mitie_tokenize_with_offsets(sentence,
                                                              ref listOffsetTokens,
                                                              ref tokens,
                                                              ref offsetTokens);

            Assert.True(listOffsetTokens.Count == 212);

            // Free unmanaged memory 
            MarshaledWrapper.Exec_mitie_free(tokens);
            MarshaledWrapper.Exec_mitie_free(offsetTokens);

        }

        [Fact]
        public void LoadNERModel()
        {
            // Load the sample text to analyze
            IntPtr ptr = new IntPtr();
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(SAMPLE_TEXT_FILE_NAME,
                                                                        ref ptr);

            IntPtr tokens = new IntPtr();
            MarshaledWrapper.Exec_mitie_tokenize(sentence, ref tokens, false);

            IntPtr ner = MarshaledWrapper.Exec_mitie_load_named_entity_extractor(SAMPLE_MITIE_NER_MODEL);

            Assert.NotNull((object)ner);

            // Free unmanaged memory 
            MarshaledWrapper.Exec_mitie_free(tokens);
            MarshaledWrapper.Exec_mitie_free(ner);

        }

        [Fact]
        public void GetNumberOfNERDetections()
        {
            // Load the sample text to analyze
            IntPtr ptr = new IntPtr();
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(SAMPLE_TEXT_FILE_NAME,
                                                                        ref ptr);

            // Tokenize the text
            IntPtr tokens = new IntPtr();
            MarshaledWrapper.Exec_mitie_tokenize(sentence, ref tokens, false);

            // Load the sample *trained* ner model for Mitie
            IntPtr ner = MarshaledWrapper.Exec_mitie_load_named_entity_extractor(SAMPLE_MITIE_NER_MODEL);

            // Calculate the detections from  the model
            IntPtr detections = MarshaledWrapper.Exec_mitie_extract_entities(ner, tokens);
            uint numDetections = MarshaledWrapper.Exec_mitie_ner_get_num_detections(detections);

            Assert.True(numDetections == 23);

            // Free unmanaged memory 
            MarshaledWrapper.Exec_mitie_free(tokens);
            MarshaledWrapper.Exec_mitie_free(ner);
            MarshaledWrapper.Exec_mitie_free(detections);

        }
    }

   

}
