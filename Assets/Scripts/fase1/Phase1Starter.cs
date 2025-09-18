// Assets/Scripts/Phase1Starter.cs
using UnityEngine;
using System.Collections.Generic;

public class Phase1Starter : MonoBehaviour
{
    [Header("Tremor (s� na crise)")]
    public UIShaker shaker;
    public float shakeAmplitude = 4f;
    public float shakeFrequency = 1.1f;

    [Header("UI (no GameDirector)")]
    public CutsceneUI_Banner ui;          // arraste o CutsceneUI_Banner

    [Header("�udio (s� na crise)")]
    public BuzzAudioController buzz;      // arraste AudioRoot
    [Range(0, 1)] public float crisisVolume = 0.45f;
    [Range(0, 1)] public float endVolume = 0.35f;
    public float buzzFade = 0.35f;

    [Header("Sprites")]
    public Sprite commonBackground;       // mesmo fundo
    public Sprite benNormal;              // personagem da apresenta��o
    public Sprite benCrisis;              // personagem da crise

    [Header("Teste")]
    public bool testCrisisOnly = false;

    void Start() { if (testCrisisOnly) { ShowCrisis(); } else { ShowIntro(); } }

    void ShowIntro()
    {
        if (buzz && buzz.source && buzz.source.isPlaying) buzz.source.Stop(); // sem zumbido

        var intro = new List<CutsceneSlideBanner>{
            new CutsceneSlideBanner{ text="Ei... Que bom que voc� chegou.", character=benNormal, bg=commonBackground },
            new CutsceneSlideBanner{ text="Meu nome � Ben. Dizem que sou especial... mas, na verdade, s� enxergo o mundo de um jeito diferente.", character=benNormal, bg=commonBackground },
            new CutsceneSlideBanner{ text="Dentro de mim existe um universo de sons, cores e sentimentos. �s vezes, � dif�cil entender. �s vezes, � dif�cil explicar.", character=benNormal, bg=commonBackground },
            new CutsceneSlideBanner{ text="Mas eu sei de uma coisa: com voc� ao meu lado, posso mostrar que ser autista tamb�m � ter um cora��o gigante.", character=benNormal, bg=commonBackground },
            new CutsceneSlideBanner{ text="Vamos juntos nessa jornada?", character=benNormal, bg=commonBackground },
            new CutsceneSlideBanner{ text="Vamos mostrar que o amor e o respeito sempre vencem o preconceito.", character=benNormal, bg=commonBackground }
        };
        ui.Show(intro, ShowCrisis);
    }

    void ShowCrisis()
    {
        if (buzz) { buzz.PlayIfStopped(); buzz.FadeTo(crisisVolume, buzzFade); }

        var crisis = new List<CutsceneSlideBanner>{
            new CutsceneSlideBanner{ text="Algo n�o est� bem�", character=benCrisis, bg=commonBackground },
            new CutsceneSlideBanner{ text="Ben est� em crise. O Transtorno do Espectro Autista est� afetando seus sentidos.", character=benCrisis, bg=commonBackground },
            new CutsceneSlideBanner{ text="Ele precisa se auto-regular. E hoje � dia de terapia�", character=benCrisis, bg=commonBackground },
            new CutsceneSlideBanner{ text="Ajude Ben a chegar at� o consult�rio da psic�loga Vit�ria.", character=benCrisis, bg=commonBackground },
            new CutsceneSlideBanner{ text="Aten��o: ele precisa chegar com a crise diminu�da.", character=benCrisis, bg=commonBackground }
        };
        if (shaker) shaker.StartShake(shakeAmplitude, shakeFrequency);

        ui.Show(crisis, OnEndCrisis);
    }

    void OnEndCrisis()
    {
        if (shaker) shaker.StopAndReset();

        if (buzz) buzz.FadeTo(endVolume, buzzFade);
        // aqui depois liberamos movimento do jogador
    }
}

