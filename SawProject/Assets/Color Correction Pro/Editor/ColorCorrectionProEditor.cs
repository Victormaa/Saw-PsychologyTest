/* Color Correction Pro ( http://www.u3d.as/DGV )
 * version 0.9.7
 * Copyright © 2017-2020 by Uniarts ( http://u3d.as/hAE )
 */

using UnityEngine;
using UnityEditor;

namespace Uniarts.ColorCorrection {
	
	[CustomEditor(typeof(ColorCorrectionPro))]
	public class ColorCorrectionProEditor : Editor {

		private Texture2D _headerTex;
		private Texture2D _separatorTex;
		private Texture2D _levelsTex;
		private Texture2D _pointerUpTex;
		private Texture2D _pointerDownTex;
		private Texture2D _hueTex;

		SerializedProperty pHue;
		SerializedProperty pSaturation;
		SerializedProperty pLightness;	

		SerializedProperty pBrightness;
		SerializedProperty pContrast;
		SerializedProperty pGamma;
		SerializedProperty pSharpness;
        SerializedProperty pTemperature;
        SerializedProperty pThreshold;

		SerializedProperty pEditColorBalance;
        SerializedProperty pRed;
		SerializedProperty pGreen;
		SerializedProperty pBlue;

		SerializedProperty pEditColorCurves;
        SerializedProperty pRedCurve;
		SerializedProperty pGreenCurve;
		SerializedProperty pBlueCurve;

        SerializedProperty pEditLevels;
		SerializedProperty pInputLevelBlack;
		SerializedProperty pInputLevelWhite;
		SerializedProperty pOutputLevelBlack;
		SerializedProperty pOutputLevelWhite;

        SerializedProperty pPhotoFilter;
		SerializedProperty pPhotoFilterMode;
        SerializedProperty pPhotoPresets;
        SerializedProperty pPhotoPreset;
        SerializedProperty pColorForPhotoFilter;
		SerializedProperty pPhotoFilterIntensity;

		SerializedProperty pLutFilter;
        SerializedProperty pLutFilterMode;
        SerializedProperty pLutPresets;
        SerializedProperty pLutPreset;
        SerializedProperty pLutTexture;
        SerializedProperty pLutFilterIntensity;

		SerializedProperty pRampFilter;
        SerializedProperty pRampFilterMode;
        SerializedProperty pRampPresets;
        SerializedProperty pRampPreset;
        SerializedProperty pRampTexture;
        SerializedProperty pRampFilterIntensity;

		SerializedProperty pFilterMode;

		SerializedProperty pCompareMode;

		static float [,] photoPresetsData = { 
			{ 0.925f, 0.541f, 0.0f }/*Warming Filter 85*/, { 0.98f, 0.541f, 0.0f }/*Warming Filter LBA*/, { 0.922f, 0.694f, 0.075f }/*Warming Filter 81*/, 
			{ 0.0f, 0.427f, 1.0f }/*Cooling Filter 80*/, { 0.0f, 0.365f, 1.0f }/*Cooling Filter LBB*/, { 0.0f, 0.71f, 1.0f }/*Cooling Filter 82*/, 

			{ 0.918f, 0.102f, 0.102f }/*Red*/, { 0.956f, 0.518f, 0.09f }/*Orange*/, { 0.976f, 0.89f, 0.11f }/*Yellow*/, 
			{ 0.098f, 0.788f, 0.098f }/*Green*/, { 0.114f, 0.796f, 0.918f }/*Cyan*/, { 0.114f, 0.209f, 0.918f }/*Blue*/, 
			{ 0.608f, 0.114f, 0.918f }/*Violet*/, { 0.89f, 0.094f, 0.89f }/*Magenta*/, { 1.0f, 1.0f, 1.0f }/*White*/, 

			{ 0.675f, 0.478f, 0.2f }/*Sepia*/, { 1.0f, 0.0f, 0.0f }/*Deep Red*/, { 0.0f, 0.133f, 0.804f }/*Deep Blue*/, 
			{ 0.0f, 0.553f, 0.0f }/*"Deep Emerald*/, { 1.0f, 0.835f, 0.0f }/*Deep Yellow*/, { 0.0f, 0.761f, 0.694f }/*Underwater*/
        };

        void OnEnable() {

            try {

                pHue = serializedObject.FindProperty("hue");
                pSaturation = serializedObject.FindProperty("saturation");
                pLightness = serializedObject.FindProperty("lightness");

                pBrightness = serializedObject.FindProperty("brightness");
                pContrast = serializedObject.FindProperty("contrast");
                pGamma = serializedObject.FindProperty("gamma");
                pSharpness = serializedObject.FindProperty("sharpness");
                pTemperature = serializedObject.FindProperty("temperature");
                pThreshold = serializedObject.FindProperty("threshold");

				pEditColorBalance = serializedObject.FindProperty("colorBalance");
                pRed = serializedObject.FindProperty("redIntensity");
                pGreen = serializedObject.FindProperty("greenIntensity");
                pBlue = serializedObject.FindProperty("blueIntensity");

				pEditColorCurves = serializedObject.FindProperty("colorCurves");
                pRedCurve = serializedObject.FindProperty("redCurve");
                pGreenCurve = serializedObject.FindProperty("greenCurve");
                pBlueCurve = serializedObject.FindProperty("blueCurve");

                pEditLevels = serializedObject.FindProperty("levels");
                pInputLevelBlack = serializedObject.FindProperty("inputLevelBlack");
                pInputLevelWhite = serializedObject.FindProperty("inputLevelWhite");
                pOutputLevelBlack = serializedObject.FindProperty("outputLevelBlack");
				pOutputLevelWhite = serializedObject.FindProperty("outputLevelWhite");

                pPhotoFilter = serializedObject.FindProperty("photoFilter");
                pPhotoFilterMode = serializedObject.FindProperty("photoFilterMode");
                pPhotoPresets = serializedObject.FindProperty("PhotoPresets");
                pPhotoPreset = serializedObject.FindProperty("photoPreset");
                pColorForPhotoFilter = serializedObject.FindProperty("colorForPhotoFilter");
                pPhotoFilterIntensity = serializedObject.FindProperty("photoFilterIntensity");

                pLutFilter = serializedObject.FindProperty("lutFilter");
                pLutFilterMode = serializedObject.FindProperty("lutFilterMode");
                pLutPresets = serializedObject.FindProperty("LutPresets");
                pLutPreset = serializedObject.FindProperty("lutPreset");
                pLutTexture = serializedObject.FindProperty("lutTexture");
                pLutFilterIntensity = serializedObject.FindProperty("lutFilterIntensity");

                pRampFilter = serializedObject.FindProperty("rampFilter");
                pRampFilterMode = serializedObject.FindProperty("rampFilterMode");
                pRampPresets = serializedObject.FindProperty("RampPresets");
                pRampPreset = serializedObject.FindProperty("rampPreset");
                pRampTexture = serializedObject.FindProperty("rampTexture");
                pRampFilterIntensity = serializedObject.FindProperty("rampFilterIntensity");

                pFilterMode = serializedObject.FindProperty("filterMode");

                pCompareMode = serializedObject.FindProperty("compareMode");

            } finally { }

		}

		void LineSeparator() {

			EditorGUILayout.Separator();

			if(_separatorTex == null) {
				if (EditorGUIUtility.isProSkin) {
					_separatorTex = Resources.Load<Texture2D> ("separatorPro");
				} else {
					_separatorTex = Resources.Load<Texture2D> ("separator");
				}
            }

			GUI.DrawTexture(GUILayoutUtility.GetRect(64, 1), _separatorTex, ScaleMode.StretchToFill );

			EditorGUILayout.Separator();

		}
			
		public override void OnInspectorGUI() {

			serializedObject.Update();

			float inputLevelBlack = pInputLevelBlack.floatValue;
			float inputLevelWhite = pInputLevelWhite.floatValue;

			float outputLevelBlack = pOutputLevelBlack.floatValue;
			float outputLevelWhite = pOutputLevelWhite.floatValue;

			if (_headerTex == null) {
				_headerTex = Resources.Load<Texture2D>("header");
            }

			if (_levelsTex == null) {
				_levelsTex = Resources.Load<Texture2D>("levels");
			}

			if (_pointerUpTex == null) {
				if (EditorGUIUtility.isProSkin) {
					_pointerUpTex = Resources.Load<Texture2D> ("pointerUpPro");
				} else {
					_pointerUpTex = Resources.Load<Texture2D> ("pointerUp");
				}
            }

			if (_pointerDownTex == null) {
				if (EditorGUIUtility.isProSkin) {
					_pointerDownTex = Resources.Load<Texture2D> ("pointerDownPro");
				} else {
					_pointerDownTex = Resources.Load<Texture2D> ("pointerDown");
				}
            }

			if (_hueTex == null) {
				_hueTex = Resources.Load<Texture2D>("hue");
			}

			if (_headerTex != null) {
				
				EditorGUILayout.Separator();

				Rect rect = new Rect();
				rect.width = 256;
				rect.height = 32;

				Rect rect2 = GUILayoutUtility.GetRect(rect.width, rect.height);

				rect.x = rect2.x;
				rect.y = rect2.y;

				GUI.DrawTexture(rect, _headerTex, ScaleMode.ScaleToFit);
					
				EditorGUILayout.Separator();

			}				

			GUI.DrawTexture(GUILayoutUtility.GetRect(9, 9), _pointerUpTex, ScaleMode.ScaleToFit );

			Rect hueRect = GUILayoutUtility.GetRect(0, 10);

			GUI.DrawTextureWithTexCoords(hueRect, _hueTex, new Rect( (pHue.floatValue + 180)/360.0f, 0f, 1.0f, 1.0f));

			GUI.DrawTexture(GUILayoutUtility.GetRect(9, 9), _pointerDownTex, ScaleMode.ScaleToFit);
								
			EditorGUILayout.PropertyField(pHue);

			EditorGUILayout.PropertyField(pSaturation);

			EditorGUILayout.PropertyField(pLightness);	

			LineSeparator();

			EditorGUILayout.PropertyField(pBrightness);
			EditorGUILayout.PropertyField(pContrast);
			EditorGUILayout.PropertyField(pGamma);

            LineSeparator();

            EditorGUILayout.PropertyField(pSharpness);
            EditorGUILayout.PropertyField(pTemperature);
            EditorGUILayout.PropertyField(pThreshold);
           
            LineSeparator();

			EditorGUILayout.PropertyField(pEditColorBalance);

			if ( pEditColorBalance.boolValue ) {
				EditorGUILayout.Separator();
				EditorGUILayout.PropertyField(pRed, new GUIContent("Cyan / Red")); 
            	EditorGUILayout.PropertyField(pGreen, new GUIContent ("Magenta / Green"));
				EditorGUILayout.PropertyField(pBlue, new GUIContent ("Yellow / Blue"));
			}

            LineSeparator();

			EditorGUILayout.PropertyField(pEditColorCurves);

			if ( pEditColorCurves.boolValue ) {
				EditorGUILayout.Separator();
				EditorGUILayout.PropertyField(pRedCurve);           
           		EditorGUILayout.Separator();

				EditorGUILayout.PropertyField(pGreenCurve);
        		EditorGUILayout.Separator();

				EditorGUILayout.PropertyField(pBlueCurve);
				EditorGUILayout.Separator();
			}

			LineSeparator();

			EditorGUILayout.PropertyField(pEditLevels);

			if ( pEditLevels.boolValue ) {

				EditorGUILayout.Separator();
                GUILayout.Label ("Input Levels");

				if (_levelsTex != null) {

					EditorGUILayout.BeginHorizontal ();
					GUILayout.Label ("", GUILayout.Width (50));
					Rect inputLevelRect = GUILayoutUtility.GetRect (0, 10);
					GUI.DrawTextureWithTexCoords (inputLevelRect, _levelsTex, new Rect ((255.0f - inputLevelBlack - inputLevelWhite) / 255.0f, 0f, 1.0f, 1.0f));
					GUILayout.Label ("", GUILayout.Width (50));
					EditorGUILayout.EndHorizontal ();

				}
					
				EditorGUILayout.BeginHorizontal ();
				inputLevelBlack = EditorGUILayout.FloatField (inputLevelBlack, GUILayout.Width (50));
				EditorGUILayout.MinMaxSlider (new GUIContent (""), ref inputLevelBlack, ref inputLevelWhite, 0.0f, 255.0f);
				inputLevelWhite = EditorGUILayout.FloatField (inputLevelWhite, GUILayout.Width (50));
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.Separator ();
				GUILayout.Label ("Output Levels");

				if (_levelsTex != null) {
					
					EditorGUILayout.BeginHorizontal ();
					GUILayout.Label ("", GUILayout.Width (50));
					Rect outputLevelRect = GUILayoutUtility.GetRect (0, 10);
					GUI.DrawTextureWithTexCoords (outputLevelRect, _levelsTex, new Rect ((outputLevelBlack + outputLevelWhite - 255.0f) / 255.0f, 0f, 1.0f, 1.0f));
					GUILayout.Label ("", GUILayout.Width (50));
					EditorGUILayout.EndHorizontal ();

				}

				EditorGUILayout.BeginHorizontal ();
				outputLevelBlack = EditorGUILayout.FloatField (outputLevelBlack, GUILayout.Width (50));
				EditorGUILayout.MinMaxSlider (new GUIContent (""), ref outputLevelBlack, ref outputLevelWhite, 0.0f, 255.0f);
				outputLevelWhite = EditorGUILayout.FloatField (outputLevelWhite, GUILayout.Width (50));
				EditorGUILayout.EndHorizontal ();   

            }

			LineSeparator();

			EditorGUILayout.PropertyField(pPhotoFilter);

			if (pPhotoFilter.boolValue) {

				EditorGUILayout.Separator();

				EditorGUILayout.PropertyField (pPhotoFilterMode, new GUIContent ("Mode"));

				if (pPhotoFilterMode.intValue == 0) {

                    pPhotoPresets = pPhotoPreset;
         
                    EditorGUILayout.PropertyField(pPhotoPresets, new GUIContent("Preset"));                 

                    pColorForPhotoFilter.colorValue = new Color(
                        photoPresetsData[pPhotoPreset.intValue, 0],
                        photoPresetsData[pPhotoPreset.intValue, 1],
                        photoPresetsData[pPhotoPreset.intValue, 2]
                    );

                    Texture2D tex = new Texture2D(212, 15);
					for (int x = 0; x < tex.width; x++) {
						for (int y = 0; y < tex.height; y++) {
							if (x == 0 || y == 0 || x == (tex.width - 1) || y == (tex.height - 1)) { 
								tex.SetPixel(x, y, new Color (0.5f, 0.5f, 0.5f)); 
							} else {
								tex.SetPixel(x, y, pColorForPhotoFilter.colorValue);
							}
						}
					}

					tex.Apply();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Color");
                    GUI.DrawTexture (GUILayoutUtility.GetRect (0, 15), tex, ScaleMode.StretchToFill);
					EditorGUILayout.EndHorizontal ();

				} else if (pPhotoFilterMode.intValue == 1) {
					EditorGUILayout.PropertyField (pColorForPhotoFilter, new GUIContent ("Color"));
				}
				
				EditorGUILayout.PropertyField (pPhotoFilterIntensity, new GUIContent ("Intensity"));

			}

			LineSeparator();

			EditorGUILayout.PropertyField(pLutFilter);

            if (pLutFilter.boolValue) {

				EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(pLutFilterMode, new GUIContent("Mode"));

                if (pLutFilterMode.intValue == 0) {

                    pLutPresets = pLutPreset;

                    EditorGUILayout.PropertyField(pLutPresets, new GUIContent("Preset"));
                  
                } else if (pLutFilterMode.intValue == 1) {

                    Texture2D lut = (Texture2D)pLutTexture.objectReferenceValue;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PrefixLabel("Lookup Texture");
                        lut = (Texture2D)EditorGUILayout.ObjectField(lut, typeof(Texture2D), false);
                    }

                    EditorGUILayout.EndHorizontal();

                    pLutTexture.objectReferenceValue = lut;
                }

                EditorGUILayout.PropertyField (pLutFilterIntensity, new GUIContent ("Intensity"));
               
            }

			LineSeparator();

			EditorGUILayout.PropertyField(pRampFilter);

			if( pRampFilter.boolValue ) {

				EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(pRampFilterMode, new GUIContent("Mode"));

                if (pRampFilterMode.intValue == 0) {

                    pRampPresets = pRampPreset;
                    EditorGUILayout.PropertyField(pRampPresets, new GUIContent("Preset"));

                } else if (pRampFilterMode.intValue == 1) {

                    Texture2D ramp = (Texture2D)pRampTexture.objectReferenceValue;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PrefixLabel("Ramp Texture");
                        ramp = (Texture2D)EditorGUILayout.ObjectField(ramp, typeof(Texture2D), false);
                    }

                    EditorGUILayout.EndHorizontal();

                    pRampTexture.objectReferenceValue = ramp;
                }

                EditorGUILayout.PropertyField(pRampFilterIntensity, new GUIContent ("Intensity"));

			}

			LineSeparator();

			EditorGUILayout.PropertyField(pFilterMode, new GUIContent ("Filter Mode"));
			EditorGUILayout.PropertyField(pCompareMode);

			LineSeparator();

			GUILayout.BeginHorizontal();
			{

				GUILayout.FlexibleSpace();

				if (GUILayout.Button ("Reset")) {
					
					ColorCorrectionPro ccp = GameObject.FindObjectOfType( typeof(ColorCorrectionPro) ) as ColorCorrectionPro;

	                ccp.enabled = false;
					ccp.Reset();
	                ccp.enabled = true;								

				}

			}

			GUILayout.EndHorizontal ();

			EditorGUILayout.Separator();

            pInputLevelBlack.floatValue = inputLevelBlack;
			pInputLevelWhite.floatValue = inputLevelWhite;

			pOutputLevelBlack.floatValue = outputLevelBlack;
			pOutputLevelWhite.floatValue = outputLevelWhite;

            serializedObject.ApplyModifiedProperties();

		}

	}

}
