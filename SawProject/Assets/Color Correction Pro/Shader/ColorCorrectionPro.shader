/* Color Correction Pro ( http://www.u3d.as/DGV )
 * version 0.9.7
 * Copyright © 2017-2020 by Uniarts ( http://u3d.as/hAE )
 */

Shader "Hidden/ColorCorrectionPro" {

	Properties {

		_MainTex ("Base (RGB)", 2D) = "" {}

		_ColorBalance("ColorBalance", int) = 0
		_Red( "Red", float) = 0
		_Green("Green", float) = 0
		_Blue("Blue", float) = 0

		_ColorCurves("ColorCurves", int) = 0
		_CurvesRGBTex ("_CurvesRGBTex Texture", 2D) = "" {}

		_Hue("Hue", float) = 0
		_Saturation("Saturation", float) = 0
		_Lightness("Lightness", float) = 0

		_Brightness("Brightness", float) = 0
		_Contrast("Contrast", float) = 0
		_Gamma("Gamma",float) = 0

		_Sharpness("Sharpness", float) = 0
		_MainTexBlurred ("Base Blurred (RGB)", 2D) = "" {}

		_Temperature("Temperature", float) = 0
		_Threshold("Threshold", float) = 0

		_Levels("Levels", int) = 0
		_InputBlack("Input Black", float) = 0
		_InputWhite("Input White", float) = 255
		_OutputBlack("Output Black", float) = 0
		_OutputWhite("Output White", float) = 255

		_PhotoFilter("Photo Filter", int) = 0
		_ColorForPhotoFilter("Color", Color) = (1, 1, 1, 1)
		_PhotoFilterIntensity("Photo Filter Intensity", float) = 0

		_LutFilter("LUT Filter", int) = 0
		_LutIntensity ("Lut Intensity", Range(0.0, 1.0)) = 1.0

		_RampFilter("Ramp Filter", int) = 0
		_RampTex ("Ramp Texture", 2D) = "white" {}
		_RampIntensity ("Ramp Intensity", Range(0.0, 1.0)) = 1.0

		_CompareMode("Compare Mode", int) = 0

	}

	CGINCLUDE

	#include "UnityCG.cginc"
     
			uniform sampler2D _MainTex;

			uniform half _Hue;
			uniform half _Saturation;
			uniform half _Lightness;
			uniform half _Brightness;
			uniform half _Contrast;
			uniform half _Gamma;

			uniform half _Sharpness;
			uniform sampler2D _MainTexBlurred;

			uniform half _Temperature;
			uniform half _Threshold;

			uniform int _ColorBalance;
			uniform half _Red;
			uniform half _Green;
			uniform half _Blue;

			uniform int _ColorCurves;
			uniform sampler2D _CurvesRGBTex;

			uniform int _Levels;
			uniform half _InputBlack;
			uniform half _InputWhite;
			uniform half _OutputWhite;
			uniform half _OutputBlack;

			uniform int _PhotoFilter;
			uniform half4 _ColorForPhotoFilter;
			uniform half _PhotoFilterIntensity;

			uniform int _LutFilter;
			uniform sampler3D _LutTex;
			uniform half _LutScale;
			uniform half _LutOffset;
			uniform half _LutIntensity;

			uniform int _RampFilter;
			uniform sampler2D _RampTex;
			uniform half _RampIntensity;

			uniform int _CompareMode;
			uniform half _NormalizeCoeffOfRange;

			inline half NormalizeCoeffOfRange(half middleValueOfRange) {
				half normalizeCoeffOfRange = middleValueOfRange/(middleValueOfRange * 3.0f + 1.0f);
				return normalizeCoeffOfRange;
			}

			// example: bringing the input values of range (0; 2) to the range (0.25, 1.75) for middleValue == 1
			inline half NormalizeValueOfRange( half currentValueOfRange ) {
				half normalizeValueOfRange = _NormalizeCoeffOfRange * (currentValueOfRange * 3.0f + 1.0f);
				return normalizeValueOfRange;
			}

			inline half3 ColorBalance(half3 sourcePixel, half redIntensity, half greenIntensity, half blueIntensity) {

				half red = sourcePixel.r;
				half green = sourcePixel.g;
				half blue = sourcePixel.b;

				half3 resultPixel = half3(red * redIntensity, green * greenIntensity, blue * blueIntensity);
									
				return resultPixel;
			}

			inline half4 GetRGBColorCurves(half4 sourcePixel) {

				half3 ycoords = half3(0.5, 1.5, 2.5) * 0.25;

				half3 redCurve = tex2D(_CurvesRGBTex, half2(sourcePixel.r, ycoords.x)).rgb * half3(1, 0, 0);
				half3 greenCurve = tex2D(_CurvesRGBTex, half2(sourcePixel.g, ycoords.y)).rgb * half3(0, 1, 0);
				half3 blueCurve = tex2D(_CurvesRGBTex, half2(sourcePixel.b, ycoords.z)).rgb * half3(0, 0, 1);

				half3 rgbCurve = redCurve + greenCurve + blueCurve;
				half4 overlayPixel = half4(lerp(sourcePixel.rgb, rgbCurve, 1.0f), sourcePixel.a);

				return overlayPixel;
			}

			inline half3 Brightness(half3 sourcePixel, half aBrightness) { return sourcePixel * aBrightness; }

			inline half4 Contrast(half4 sourcePixel, half aContrast) {

				half3 resultPixel = lerp(half3(0.5, 0.5, 0.5), sourcePixel.rgb, aContrast );

				return half4(resultPixel, sourcePixel.a);
			}

			inline half4 Gamma(half4 sourcePixel, half aGamma) { return pow(sourcePixel, aGamma); }

			inline half4 Hue(half4 sourcePixel, half aHue) {

					half angle = radians(aHue);
					half4 coeff = half4(0.57735, 0.57735, 0.57735, 1.0);

					half3 resultPixel = coeff * dot(coeff, sourcePixel.rgb) * (1 - cos(angle)) +
								  			cross(coeff, sourcePixel.rgb) * sin(angle) +
								  			  			 sourcePixel.rgb  * cos(angle);

					return  half4 (resultPixel, sourcePixel.a);
			}

			inline half3 Saturation(half3 sourcePixel, half aSaturation) {
				half luminance = dot(sourcePixel, half3(0.22, 0.707, 0.071));
				half3 resultPixel = lerp( luminance, sourcePixel, aSaturation);
				return resultPixel;
			}

			inline half4 Lightness(half4 sourcePixel, half aLightness) {
		
				half4 lightFilter;

				if(aLightness >= 1.0f) {
					lightFilter = half4(1.0f, 1.0f, 1.0f, 1.0f); // white
				} else if (aLightness < 1.0f) {
					lightFilter = half4(0.0f, 0.0f, 0.0f, 1.0f); // black
				}

				half4 resultPixel = lerp( -1.0f * lightFilter, sourcePixel, aLightness * 0.5 + 0.5f  );

				return resultPixel;
			}

			inline half4 Sharpness(half4 sourcePixel, half4 blurred, half aSharpness) {

				half4 difference = sourcePixel - blurred;
				half4 signs = sign (difference);
				
				half4 enhancement = saturate (abs(difference)) * signs;
				sourcePixel += enhancement * aSharpness;

				return sourcePixel;
			}

			inline half3 Temperature(half3 sourcePixel, half temperature) {

				half3 resultPixel;
				
				half r = sourcePixel.r;
				half g = sourcePixel.g;
				half b = sourcePixel.b;

				if(temperature > 0) {
					resultPixel = half3(r * temperature, g, b / temperature );
				} else if(temperature < 0) {
					resultPixel = half3(r / temperature, g, b * temperature );
				}
								
				return resultPixel;
			}

			inline half3 Threshold(half3 sourcePixel, half threshold) {

				half r = sourcePixel.r;
				half g = sourcePixel.g;
				half b = sourcePixel.b;

				if (r < threshold) { r = 0.0f; }
				if (r > (1.0f - threshold)) { r = 1.0f; }
				if (g < threshold) { g = 0.0f; }
				if (g > (1.0f - threshold)) { g = 1.0f; }
				if (b < threshold) { b = 0.0f; }
				if (b > (1.0f - threshold)) { b = 1.0f; }

				return half3(r, g, b);
			}

			inline half3 BlackAndWhiteIOLevels(half3 sourcePixel, half aInputBlack, half aInputWhite, half aOutputBlack, half aOutputWhite) {

				half3 resultPixel;
													
				resultPixel = (sourcePixel.rgb * 255.0);
				resultPixel = max(0, resultPixel - aInputBlack);
				resultPixel = saturate(resultPixel / (aInputWhite - aInputBlack));
				resultPixel = (resultPixel * (aOutputWhite - aOutputBlack) + aOutputBlack) / 255.0;
												
				return resultPixel;
			}

			inline half4 PhotoFilter(half4 sourcePixel, half4 aColor, half aPhotoFilterIntensity) {

				half luminance = Luminance(sourcePixel.rgb);

				half4 resultPixel = lerp(sourcePixel,
										 aColor * 0.5f + luminance * 1.25f - half4(0.333, 0.333, 0.333, 1.0),
										 aPhotoFilterIntensity * 0.5f );

				return resultPixel;
			}

			inline half4 LUTFilter(half4 sourcePixel, half aLutIntensity) {
				half3 lutPixel = tex3D(_LutTex, sourcePixel.rgb * _LutScale + _LutOffset).rgb;
				return lerp(sourcePixel, half4(lutPixel, sourcePixel.a), aLutIntensity);
			}

			inline half4 RampFilter( half4 sourcePixel, half aRampIntensity ) {
				half luminance = Luminance(sourcePixel.rgb);
				half4 ramp = tex2D(_RampTex, luminance);
				return lerp(sourcePixel, ramp, aRampIntensity);
			}

			inline half4 SetFilter(half4 sourcePixel) {

				if (_PhotoFilter == 1) {
					sourcePixel = PhotoFilter(sourcePixel, _ColorForPhotoFilter, _PhotoFilterIntensity );
				}

				if (_LutFilter == 1) {
					sourcePixel = LUTFilter(sourcePixel, _LutIntensity);
				}

				if (_RampFilter == 1) {
					sourcePixel = RampFilter(sourcePixel, _RampIntensity);
				}

				return sourcePixel;
			}

			half4 frag (v2f_img i): COLOR {
						
				_NormalizeCoeffOfRange = NormalizeCoeffOfRange(1.0f);

				// i.uv - coords
				half4 pixelColor = tex2D( _MainTex, i.uv );
				half4 pixelColorSource = pixelColor;

				half4 blurred = tex2D (_MainTexBlurred, i.uv );
				if ( _Sharpness != 0.0f ) { pixelColor = Sharpness( pixelColor, blurred, _Sharpness ); }
				
				pixelColor = SetFilter(pixelColor);
				if ( _Gamma != 1.0f ) { pixelColor = Gamma( pixelColor, NormalizeValueOfRange( _Gamma ) ); }

				if ( _Brightness != 1.0f ) { pixelColor.rgb = Brightness( pixelColor.rgb, NormalizeValueOfRange(_Brightness) ); }
				if ( _Contrast != 1.0f ) { pixelColor = Contrast( pixelColor, NormalizeValueOfRange( _Contrast ) ); }

				if ( _Hue != 0.0f ) { pixelColor = Hue( pixelColor, _Hue ); }
				if ( _Saturation != 1.0f ) { pixelColor.rgb = Saturation( pixelColor.rgb, _Saturation ); }		
				if ( _Lightness != 1.0f ) { pixelColor = Lightness( pixelColor, NormalizeValueOfRange( _Lightness ) ); }

				if ( _Temperature != 1.0f ) { pixelColor.rgb = Temperature(pixelColor.rgb, NormalizeValueOfRange(_Temperature) ); }
				if ( _Threshold != 0.0f ) { pixelColor.rgb = Threshold(pixelColor.rgb, _Threshold); }

				if ( _ColorBalance == 1 ) {
					pixelColor.rgb = ColorBalance(pixelColor.rgb, _Red, _Green, _Blue);
				}

				if ( _ColorCurves == 1 ) {
					pixelColor = GetRGBColorCurves( pixelColor );
				}

				if ( _Levels == 1 ) {
					pixelColor.rgb = BlackAndWhiteIOLevels( pixelColor.rgb, _InputBlack, _InputWhite, _OutputBlack, _OutputWhite );
				}

				if ( _CompareMode == 1 ) {
					return i.uv.x > 0.5 ? pixelColor : pixelColorSource;
				}

				return pixelColor;

			}

			half4 fragLinear(v2f_img i): COLOR {

				_NormalizeCoeffOfRange = NormalizeCoeffOfRange(1.0f);

				// i.uv - coords
				half4 pixelColor = tex2D( _MainTex, i.uv );
				half4 pixelColorSource = pixelColor;

				half4 blurred = tex2D (_MainTexBlurred, i.uv );
				if ( _Sharpness != 0.0f ) { pixelColor = Sharpness( pixelColor, blurred, _Sharpness ); }

					pixelColor.rgb = sqrt(pixelColor.rgb);

				pixelColor = SetFilter(pixelColor);
				if ( _Gamma != 1.0f ) { pixelColor = Gamma( pixelColor, NormalizeValueOfRange( _Gamma ) ); }

				if ( _Brightness != 1.0f ) { pixelColor.rgb = Brightness( pixelColor.rgb, NormalizeValueOfRange(_Brightness) ); }
				if ( _Contrast != 1.0f ) { pixelColor = Contrast( pixelColor, NormalizeValueOfRange( _Contrast ) ); }

				if ( _Hue != 0.0f ) { pixelColor = Hue( pixelColor, _Hue ); }
				if ( _Saturation != 1.0f ) { pixelColor.rgb = Saturation( pixelColor.rgb, _Saturation ); }
				if ( _Lightness != 1.0f ) { pixelColor = Lightness( pixelColor, NormalizeValueOfRange( _Lightness ) ); }

				if ( _Temperature != 1.0f ) { pixelColor.rgb = Temperature(pixelColor.rgb, NormalizeValueOfRange(_Temperature)); }
				if ( _Threshold != 0.0f ) { pixelColor.rgb = Threshold(pixelColor.rgb, _Threshold); }

				if ( _ColorBalance == 1 ) {
					pixelColor.rgb = ColorBalance(pixelColor.rgb, _Red, _Green, _Blue);
				}

				if ( _ColorCurves == 1 ) {
					pixelColor = GetRGBColorCurves( pixelColor );
				}

				if ( _Levels == 1 ) {
					pixelColor.rgb = BlackAndWhiteIOLevels( pixelColor.rgb, _InputBlack, _InputWhite, _OutputBlack, _OutputWhite );
				}

					pixelColor.rgb *= pixelColor.rgb;

				if (_CompareMode == 1) {
					return i.uv.x > 0.5 ? pixelColor : pixelColorSource;
				}

				return pixelColor;

			}

	ENDCG

	SubShader {

		Pass {
			ZTest Always Cull Off ZWrite Off
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			ENDCG
		}

		Pass {
			ZTest Always Cull Off ZWrite Off
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragLinear
			#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			ENDCG
		}
			
	}

	Fallback off

}
