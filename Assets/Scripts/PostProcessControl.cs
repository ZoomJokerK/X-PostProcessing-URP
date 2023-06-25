using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;
using DG.Tweening;
using XPostProcessing;



public class PostProcessControl : MonoBehaviour
{
    public Volume postProcessVolume;

//***********************************************************************************
//***********************************************************************************
//some attributes its better to set with inspector,if u want to apply it in runtime,change the code urselves,its easy

    [Button("PixelizeLed--像素化")]
    public void Trigger_PixelizeLed(float duration,float pixelSize=0.6f)
    {
        postProcessVolume.profile.TryGet(out PixelizeLed pixelizeLed);
        DOTween.Kill(typeof(PixelizeLed).FullName);
        pixelizeLed.active = true;
        DOTween.To(() => pixelizeLed.pixelSize.value, x => pixelizeLed.pixelSize.value = x, pixelSize, duration)
        .SetEase(Ease.OutCubic).SetId(typeof(PixelizeLed).FullName)
        .OnComplete(() => {
                pixelizeLed.pixelSize.value = 0;
                pixelizeLed.active = false;
        })
        .OnKill(()=> {
                pixelizeLed.pixelSize.value = 0;
                pixelizeLed.active = false;
            
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutCubic);
    }
    [Button("ColorBlink--闪屏")]
    public void Trigger_ColorBlink(float duration,Color color=default(Color))
    {

        postProcessVolume.profile.TryGet(out BlackWhite colorBlink);
        if(color==default(Color))
        {
            color=Color.white;
        }
        colorBlink.TintColor.value = color;

        DOTween.Kill(typeof(BlackWhite).FullName);
        colorBlink.active = true;
        colorBlink.Enable.value = true;
        //简单的dottween计时器来控制colorBlink.active
        float timer=0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(BlackWhite).FullName)
        .OnKill(() => {
            colorBlink.active = false;
        }).OnComplete(()=> {
            colorBlink.active = false;
        });
    }
    [Button("ColorSplit_1--色彩分离_1")]
    public void Trigger_ColorSplit_1(float duration)
    {
        postProcessVolume.profile.TryGet(out GlitchRGBSplitV2 colorSplit);

        DOTween.Kill(typeof(GlitchRGBSplitV2).FullName);
        colorSplit.active = true;
        float timer=0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchRGBSplitV2).FullName)
        .OnKill(() => {
            colorSplit.active = false;
        }).OnComplete(() => {
            colorSplit.active = false;
        });

    }
    [Button("ColorSplit_2--色彩分离_2")]
    public void Trigger_ColorSplit_2(float duration)
    {
        postProcessVolume.profile.TryGet(out GlitchRGBSplitV3 colorSplit);
        DOTween.Kill(typeof(GlitchRGBSplitV2).FullName);
        colorSplit.active = true;
        float timer = 0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchRGBSplitV2).FullName)
        .OnKill(() => {
            colorSplit.active = false;
        }).OnComplete(() => {
            colorSplit.active = false;
        });
    }
    [Button("ScreenJump--跳屏")]
    public void Trigger_ScreenJump(float duration,float indensity=0.2f)
    {
        postProcessVolume.profile.TryGet(out GlitchScreenJump screenJump);
        if(indensity>0)
        {
            //check: indensity is hard to adjust better adjust by inspector
            screenJump.ScreenJumpIndensity.value = indensity;
        }

        DOTween.Kill(typeof(GlitchScreenJump).FullName);
        screenJump.active = true;
        float timer = 0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchScreenJump).FullName)
        .OnKill(() => {
            screenJump.active = false;
        }).OnComplete(() => {
            screenJump.active = false;
        });
    }

    [Button("EyeBlur--眼睛模糊")]
    public void Trigger_EyeBlur(float duration,int LoopTime=2)
    {
        postProcessVolume.profile.TryGet(out KawaseBlur eyeBlur);
        DOTween.Kill(typeof(KawaseBlur).FullName);
        eyeBlur.active = true;
        DOTween.To(() => eyeBlur.Iteration.value, x => eyeBlur.Iteration.value = x, 15, duration).SetId(typeof(KawaseBlur).FullName)
        .OnKill(() => {
            eyeBlur.active = false;
        }).OnComplete(() => {
            eyeBlur.active = false;
        })
        .SetLoops(LoopTime, LoopType.Yoyo);
    }
    [Button("NoiseBlur--噪点模糊")]
    public void Trigger_NoiseBlur(float duration,int LoopTime=2)
    {
        postProcessVolume.profile.TryGet(out GrainyBlur noiseBlur);
        DOTween.Kill(typeof(GrainyBlur).FullName);
        noiseBlur.active = true;
        DOTween.To(() => noiseBlur.BlurRadius.value, x => noiseBlur.BlurRadius.value = x, 22, duration).SetId(typeof(GrainyBlur).FullName)
        .OnKill(() => {
            noiseBlur.active = false;
        }).OnComplete(() => {
            noiseBlur.active = false;
        })
        .SetLoops(LoopTime, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);//keep the blur radius in a high value
    }

    [Button("ScreenLine--屏幕花线")]
    public void Trigger_ScreenLine(float duration)
    {
        postProcessVolume.profile.TryGet(out GlitchScanLineJitter screenLine);
        DOTween.Kill(typeof(GlitchScanLineJitter).FullName);
        screenLine.active = true;

        float timer = 0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchScanLineJitter).FullName)
        .OnKill(() => {
            screenLine.active = false;
        }).OnComplete(() => {
            screenLine.active = false;
        });
    }
    [Button("ScreenWaveJitter--屏幕波纹抖动")]
    public void Trigger_ScreenWaveJitter(float duration)
    {
        postProcessVolume.profile.TryGet(out GlitchWaveJitter screenWaveJitter);
        DOTween.Kill(typeof(GlitchWaveJitter).FullName);
        screenWaveJitter.active = true;
        float timer = 0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchWaveJitter).FullName)
        .OnKill(() => {
            screenWaveJitter.active = false;
        }).OnComplete(() => {
            screenWaveJitter.active = false;
        });
    }
    [Button("EdgeOutLine--边缘描边")]
    //this one doesn't perform well  better find another way to do edge outline
    public void Trigger_EdgeOutLine(bool active)
    {
        postProcessVolume.profile.TryGet(out EdageOutline edgeOutLine);
        edgeOutLine.active = active;
    }
    [Button("Halo--光晕")]
    //i think this one its for damege effect
    public void Trigger_Halo(float duration=.6f,float maxIndensity=0.5f)
    {
        postProcessVolume.profile.TryGet(out RapidVignette halo);
        DOTween.Kill(typeof(RapidVignette).FullName);
        halo.active = true;
        DOTween.To(() => halo.vignetteIndensity.value, x => halo.vignetteIndensity.value = x, maxIndensity, duration).SetId(typeof(RapidVignette).FullName)
        .OnKill(() => {
            halo.active = false;
        }).OnComplete(() => {
            halo.active = false;
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);
    }
    [Button("Brightness--亮度")]
    public void Trigger_Brightness(float duration,bool lighterOrDarker=true)
    {
        postProcessVolume.profile.TryGet(out ColorAdjustmentBrightness brightness);
        float target=lighterOrDarker?1:-1;
        DOTween.Kill(typeof(ColorAdjustmentBrightness).FullName);
        brightness.active = true;
        brightness.Indensity.value = 0;
        DOTween.To(() => brightness.Indensity.value, x => brightness.Indensity.value = x, target, duration).SetId(typeof(ColorAdjustmentBrightness).FullName)
        .OnKill(() => {
            brightness.active = false;
        }).OnComplete(() => {
            brightness.active = false;
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);
    }

    [Button("Color_Cold--冷色调")]
    public void Trigger_ColorCold(float duration)
    {
        postProcessVolume.profile.TryGet(out ColorAdjustmentBleachBypass colorCold);
        DOTween.Kill(typeof(ColorAdjustmentBleachBypass).FullName);
        colorCold.active = true;
        colorCold.Indensity.value = 0;
        DOTween.To(() => colorCold.Indensity.value, x => colorCold.Indensity.value = x, 1, duration).SetId(typeof(ColorAdjustmentBleachBypass).FullName)
        .OnKill(() => {
            colorCold.active = false;
        }).OnComplete(() => {
            colorCold.active = false;
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);
    }
    [Button("ColorReplace--颜色替换")]
    //this method may need color adjust in code 
    //but we dont have something call hdrcolor like color to save the color info
    public void Trigger_ColorReplace(float duration)
    {
        postProcessVolume.profile.TryGet(out ColorAdjustmentColorReplace colorReplace);
        DOTween.Kill(typeof(ColorAdjustmentColorReplace).FullName);
        colorReplace.active = true;
        colorReplace.Range.value = 0;
        DOTween.To(() => colorReplace.Range.value, x => colorReplace.Range.value = x, 1, duration).SetId(typeof(ColorAdjustmentColorReplace).FullName)
        .OnKill(() => {
            colorReplace.active = false;
        }).OnComplete(() => {
            colorReplace.active = false;
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);
    }
    [Button("ScreenGray--屏幕灰色")]
    public void Trigger_ScreenGray(float duration)
    {
        postProcessVolume.profile.TryGet(out ScreenBinarization screenGray);
        DOTween.Kill(typeof(ScreenBinarization).FullName);
        screenGray.active = true;
        screenGray.intensity.value = 0;
        DOTween.To(() => screenGray.intensity.value, x => screenGray.intensity.value = x, 1, duration).SetId(typeof(ScreenBinarization).FullName)
        .OnKill(() => {
            screenGray.active = false;
        }).OnComplete(() => {
            screenGray.active = false;
        })
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutExpo);
    }
    [Button("ColorHue--颜色滚动")]
    public void Trigger_ColorHue(float duration)
    {
        postProcessVolume.profile.TryGet(out ColorAdjustmentHue colorHue);
        DOTween.Kill(typeof(ColorAdjustmentHue).FullName);
        colorHue.active = true;
        colorHue.HueDegree.value = -180;
        DOTween.To(() => colorHue.HueDegree.value, x => colorHue.HueDegree.value = x, 180, duration).SetId(typeof(ColorAdjustmentHue).FullName)
        .OnKill(() => {
            colorHue.active = false;
        }).OnComplete(() => {
            colorHue.active = false;
        })
        .SetLoops(2, LoopType.Yoyo);
        //.SetEase(Ease.OutExpo);
    }
    [Button("AwakeEyes--睁眼")]
    public void Trigger_AwakeEyes(float duration)
    {
        postProcessVolume.profile.TryGet(out AwakingEye awakeEyes);
        DOTween.Kill(typeof(AwakingEye).FullName);
        awakeEyes.active = true;
        awakeEyes.awakingEyeOpenValue.value = 0;
        DOTween.To(() => awakeEyes.awakingEyeOpenValue.value, x => awakeEyes.awakingEyeOpenValue.value = x, 1, duration).SetId(typeof(AwakingEye).FullName)
        .OnKill(() => {
            awakeEyes.active = false;
            awakeEyes.awakingEyeOpenValue.value = 0;
        }).OnComplete(() => {
            awakeEyes.active = false;
            awakeEyes.awakingEyeOpenValue.value = 0;
        });
    }
    [Button("ImageBlock--图块故障")]
    public void Trigger_ImageBlock(float duration)
    {
        postProcessVolume.profile.TryGet(out GlitchImageBlockV4 imageBlock);
        DOTween.Kill(typeof(GlitchImageBlockV4).FullName);
        imageBlock.active = true;
        float timer = 0;
        DOTween.To(() => timer, x => timer = x, 0, duration).SetId(typeof(GlitchImageBlockV4).FullName)
        .OnKill(() => {
            imageBlock.active = false;
        }).OnComplete(() => {
            imageBlock.active = false;
        });
    }

/*
    // i think its better to add by inspector if u lost the profile
    //many attributes need to be set by urself
    public void EnsureProfileCreated()
    {

    }
    */
    
}

