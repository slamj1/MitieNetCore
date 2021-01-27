using MitieNetCore.Wrappers;
using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr ptr = new IntPtr();

            // Load a sentence from a sample file
            var sentence = MarshaledWrapper.Exec_mitie_load_entire_file(@"..\..\..\..\sample1.txt",
                                                                        ref ptr);
            
            Console.WriteLine($"Loaded sentence -> {sentence}\n");

            IntPtr tokens = new IntPtr();
            IntPtr offsetTokens = new IntPtr();
            List<uint> listOffsetTokens = new List<uint>();

            // Gte the tokens and token offsets
            var listTokens = MarshaledWrapper.Exec_mitie_tokenize_with_offsets(sentence,
                                                                               ref listOffsetTokens,
                                                                               ref tokens,
                                                                               ref offsetTokens,
                                                                               false);
            foreach (var token in listTokens) 
            {
                Console.WriteLine($"Token -> {token}");
            }

            Console.WriteLine("\n");
            // Load the sample *trained* model
            IntPtr entityExtractto = MarshaledWrapper.Exec_mitie_load_named_entity_extractor(@"..\..\..\..\ner_model.dat");

            // Get the mumber of detections
            IntPtr detections = MarshaledWrapper.Exec_mitie_extract_entities(entityExtractto, tokens);
            long numDetections = MarshaledWrapper.Exec_mitie_ner_get_num_detections(detections);

            Console.WriteLine($"Number of detections -> {numDetections}");

            // Free the unmanaged memory
            MarshaledWrapper.Exec_mitie_free(tokens);
            MarshaledWrapper.Exec_mitie_free(offsetTokens);
            MarshaledWrapper.Exec_mitie_free(entityExtractto);
            MarshaledWrapper.Exec_mitie_free(detections);
        }
    }
}
   