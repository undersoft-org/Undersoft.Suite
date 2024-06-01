/*
 * config.h
 *
 *  Created on: 31 mar 2021
 * 
 */

#ifndef _CONFIG_H_
#define _CONFIG_H_

#include <climits>
#include <stdint.h>

 // Incorporate appropriate tuneups when the NCBI C++ Toolkit's core
 // headers have been included.
 //


 // macro to define/undefine unaligned memory access (x86, PowerPC)
 //
#if defined(__i386) || defined(__x86_64) || defined(__ppc__) || \
    defined(__ppc64__) || defined(_M_IX86) || defined(_M_AMD64) || \
    defined(_M_IX86) || defined(_M_AMD64) || defined(_M_X64) || \
    defined(_M_ARM) || defined(_M_ARM64) || \
    defined(__arm__) || defined(__aarch64__) || \
    (defined(_M_MPPC) && !defined(_FORBID_UNALIGNED_ACCESS))
#define _UNALIGNED_ACCESS_OK 1
#endif

#if defined(_M_IX86) || defined(_M_AMD64) || defined(_M_X64) || \
    defined(__i386) || defined(__x86_64) || defined(_M_AMD64) || \
    defined(SSE2OPT) || defined(SSE42OPT)
#define _x86
#endif

// cxx11 features
//
#if defined(_NO_CXX11) || (defined(_MSC_VER)  &&  _MSC_VER < 1900)
# define NOEXCEPT
# define NOEXCEPT2
#else
# ifndef NOEXCEPT
#  define NOEXCEPT noexcept
#if defined(__EMSCRIPTEN__)
#else
#  define NOEXCEPT2
#endif
# endif
#endif

// WebASM compilation settings
//
#if defined(__EMSCRIPTEN__)

// EMSCRIPTEN specific tweaks
// WebAssemply compiles into 32-bit memory system but offers 64-bit wordsize
// WebASM also benefits from use GCC extensions (buildins like popcnt, lzcnt)
//
// NOEXCEPT2 is to declare "noexcept" for WebAsm only where needed
// and silence GCC warnings
//
# define OPT64
# define _USE_GCC_BUILD
# define NOEXCEPT2 noexcept

#else
#  define NOEXCEPT2
#endif


// Enable MSVC 8.0 (2005) specific optimization options
//
#if(_MSC_VER >= 1400)
#  define _HASFORCEINLINE
#  ifndef RESTRICT
#    define RESTRICT __restrict
#  endif
#endif

#ifdef __GNUG__
#  ifndef RESTRICT
#    define RESTRICT __restrict__
#  endif

#  ifdef __OPTIMIZE__
#    define _NOASSERT
#  endif
#endif

# ifdef NDEBUG
#    define _NOASSERT
# endif



#if defined(__x86_64) || defined(_M_AMD64) || defined(_WIN64) || \
    defined(__LP64__) || defined(_LP64) || ( __WORDSIZE == 64 )
#ifndef OPT64
# define OPT64
#endif
#endif




#ifdef _HASRESTRICT
# ifndef RESTRICT
#  define RESTRICT restrict
# endif
#else
# ifndef RESTRICT
#   define RESTRICT
# endif
#endif

#ifndef FORCEINLINE
#ifdef _HASFORCEINLINE
# ifndef FORCEINLINE
#  define FORCEINLINE __forceinline
# endif
#else
# define FORCEINLINE inline
#endif
#endif


// --------------------------------
// SSE optmization macros
//

#ifdef SSE42OPT
# if defined(OPT64) || defined(__x86_64) || defined(_M_AMD64) || defined(_WIN64) || \
    defined(__LP64__) || defined(_LP64) || ( __WORDSIZE == 64 )
#   undef OPT64
#   define OPT64_SSE4
# endif
# undef SSE2OPT
#endif

#ifdef AVX2OPT
# if defined(OPT64) || defined(__x86_64) || defined(_M_AMD64) || defined(_WIN64) || \
    defined(__LP64__) || defined(_LP64)
#   undef OPT64
#   undef OPT64_SSE4
#   define OPT64_AVX2
# endif
# undef SSE2OPT
# undef SSE42OPT
#endif

#ifdef AVX512OPT
# if defined(OPT64) || defined(__x86_64) || defined(_M_AMD64) || defined(_WIN64) || \
    defined(__LP64__) || defined(_LP64)
#   undef OPT64
#   undef OPT64_SSE4
#   undef OPT64_AVX2
#   define OPT64_AVX512
# endif
# undef SSE2OPT
# undef SSE42OPT
#endif

# ifndef _SET_MMX_GUARD
#  define _SET_MMX_GUARD
# endif

#endif /* _CONFIG_H_ */
