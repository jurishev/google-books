export interface Volume {
    id: string,
    etag: string,
    selfLink: string,
    volumeInfo: {
        title: string,
        authors: string[],
        publisher: string,
        publishedDate: string,
        description: string,
        imageLinks: {
            smallThumbnail: string,
            thumbnail: string
        }
    }
}
