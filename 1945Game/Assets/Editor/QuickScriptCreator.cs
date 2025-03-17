using System.IO;
using UnityEditor;
using UnityEngine;

public class QuickScriptCreator
{
    [MenuItem("Assets/Create/Quick MonoBehaviour Script %#m", false, 50)]
    public static void CreateScript()
    {
        // 이름 입력 창 표시
        string defaultName = "NewMonoBehaviour";
        string scriptName = EditorUtility.SaveFilePanel(
            "Create MonoBehaviour Script",
            GetSelectedPath(),
            defaultName,
            "cs"
        );

        if (string.IsNullOrEmpty(scriptName))
            return; // 취소하면 종료

        // 파일 경로를 유니티 프로젝트 상대 경로로 변환
        string relativePath = "Assets" + scriptName.Substring(Application.dataPath.Length);

        // 확장자가 없는 경우 .cs 추가
        if (!relativePath.EndsWith(".cs"))
            relativePath += ".cs";

        // 같은 이름의 파일이 있으면 덮어쓰기 방지
        if (File.Exists(relativePath))
        {
            Debug.LogError("이미 존재하는 파일입니다.");
            return;
        }

        // 클래스명 추출 (파일명에서 .cs 제거)
        string className = Path.GetFileNameWithoutExtension(relativePath);

        // 스크립트 기본 템플릿
        string scriptContent =
            "using UnityEngine;\n\n"
            + $"public class {className} : MonoBehaviour\n"
            + "{\n"
            + "    void Start()\n"
            + "    {\n"
            + "        \n"
            + "    }\n\n"
            + "    void Update()\n"
            + "    {\n"
            + "        \n"
            + "    }\n"
            + "}";

        // 파일 생성
        File.WriteAllText(relativePath, scriptContent);
        AssetDatabase.Refresh();

        // 생성된 파일 선택
        Object obj = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
        Selection.activeObject = obj;
    }

    // 현재 선택한 폴더 경로 가져오기
    private static string GetSelectedPath()
    {
        string path = "Assets"; // 기본값

        if (Selection.activeObject != null)
        {
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(assetPath))
            {
                if (Directory.Exists(assetPath))
                    return assetPath; // 폴더 선택됨
                return Path.GetDirectoryName(assetPath); // 파일 선택됨 -> 해당 파일이 있는 폴더 사용
            }
        }

        return path;
    }
}
