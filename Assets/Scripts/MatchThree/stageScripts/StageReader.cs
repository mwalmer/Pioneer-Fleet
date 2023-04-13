using UnityEngine;

namespace MatchThree.Stage
{
    public static class StageReader
    {
        public static StageInfo LoadStage(int nStage)
        {
            //read text
            TextAsset textAsset = Resources.Load<TextAsset>($"Stage/{GetFileName(nStage)}");
            if (textAsset != null)
            {
                StageInfo stageInfo = JsonUtility.FromJson<StageInfo>(textAsset.text);
                return stageInfo;
            }

            return null;
        }

//get stage file name
        static string GetFileName(int nStage)
        {
            return string.Format("stage_{0:D4}", nStage);
        }
    }
}
