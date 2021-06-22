using UnityEngine;

namespace DataStructures {
    
    public class ExerJsonLoader {
        private TextAsset jsonFile;
        
        public ExerJsonLoader(TextAsset file) {
            jsonFile = file;
        }

        public Exercises LoadExercises() {
            return JsonUtility.FromJson<Exercises>(jsonFile.text);
        }

    }
}
